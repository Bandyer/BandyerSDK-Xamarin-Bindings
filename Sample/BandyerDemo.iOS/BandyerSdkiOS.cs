﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Bandyer;
using BandyerDemo.iOS;
using CallKit;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using PushKit;
using UIKit;
using Xamarin.Forms;
using Intents;

[assembly: Dependency(typeof(BandyerSdkiOS))]
namespace BandyerDemo.iOS
{
    public class BandyerSdkiOS : NSObject
        , IBandyerSdk
        , IBCXCallClientObserver
        , IBDKCallWindowDelegate
        , IBCHChatClientObserver
        , IBCHChannelViewControllerDelegate
        , IPKPushRegistryDelegate
        , IBCHMessageNotificationControllerDelegate
        , IBDKCallBannerControllerDelegate
    {
        public const string AppId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

        private static BandyerSdkiOS instance = null;
        public BandyerSdkiOS()
        {
            instance = this;
        }

        private BDKCallWindow callWindow = null;
        private string callUserAlias;
        private string chatUserAlias;
        private string currentUserAlias;
        private IBCHChatClient chatClient;
        private BCHMessageNotificationController messageNotificationController;
        private BDKCallBannerController callBannerController;

        public static void InitSdk()
        {
            instance.InitSdkInt();
        }

        public static bool ContinueUserActivity(NSUserActivity userActivity)
        {
            return instance.ContinueUserActivityInt(userActivity);
        }

        void InitSdkInt()
        {
            var config = new BDKConfig();
            config.NotificationPayloadKeyPath = "data";
            config.PushRegistryDelegate = this;
            config.Environment = BDKEnvironment.Sandbox;

            // CALLKIT
            config.CallKitEnabled = true;
            config.NativeUILocalizedName = "My wonderful app";
            //config.NativeUIRingToneSound = "MyRingtoneSound";
            UIImage callKitIconImage = UIImage.FromBundle("bandyer_logo");
            config.NativeUITemplateIconImageData = callKitIconImage.AsPNG();
            config.SupportedHandleTypes = new NSSet(new object[] { CXHandleType.EmailAddress, CXHandleType.Generic });
            config.HandleProvider = new BandyerSdkBCXHandleProvider();
            // CALLKIT

            BandyerSDK.Instance().InitializeWithApplicationId(AppId, config);
        }

        bool ContinueUserActivityInt(NSUserActivity userActivity)
        {
            if (userActivity.ActivityType == NSUserActivityType.BrowsingWeb)
            {
                var url = userActivity.WebPageUrl;
                var intent = BDKJoinURLIntent.IntentWithURL(url);
                initCallWindow();
                callWindow.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
                {
                    if (!success)
                    {
                        var alert = UIAlertController.Create("Warning", "Another call is already in progress.", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                        UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
                    }
                });

                return true;
            } else if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0) && userActivity.GetInteraction()?.Intent != null)
            {
                return HandleINIntent(userActivity.GetInteraction()?.Intent); 
            }

            return false;
        }

        [Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
        private bool HandleINIntent(INIntent intent)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                if (intent.GetType() == typeof(INStartCallIntent))
                {
                    HandleINStartCallIntent((INStartCallIntent)intent);
                    return true;
                }
                else if (intent.GetType() == typeof(INStartVideoCallIntent))
                {
                    HandleINStartVideoCallIntent((INStartVideoCallIntent)intent);
                    return true;
                }
            }
            else
            {
                if (intent.GetType() == typeof(INStartVideoCallIntent))
                {
                    HandleINStartVideoCallIntent((INStartVideoCallIntent)intent);
                    return true;
                }
            }

            return false;
        }

        [Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
        private void HandleINStartVideoCallIntent(INStartVideoCallIntent intent)
        {
            initCallWindow();
            callWindow.HandleINStartVideoCallIntent(intent);
        }

        [Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
        private void HandleINStartCallIntent(INStartCallIntent intent)
        {
            initCallWindow();
            callWindow.HandleINStartCallIntent((INStartCallIntent)intent);
        }

        #region IBandyerSdk
        public event Action CallReadyEvent;
        public event Action ChatReadyEvent;

        public void Init(string userAlias)
        {
            this.currentUserAlias = userAlias;

            BandyerSDK.Instance().CallClient.AddObserver(this, DispatchQueue.MainQueue);
            BandyerSDK.Instance().CallClient.Start(currentUserAlias);

            BandyerSDK.Instance().ChatClient.AddObserver(this, DispatchQueue.MainQueue);
            BandyerSDK.Instance().ChatClient.Start(currentUserAlias);

            messageNotificationController = new BCHMessageNotificationController();
            messageNotificationController.Delegate = this;
            messageNotificationController.ParentViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            callBannerController = new BDKCallBannerController();
            callBannerController.Delegate = this;
            callBannerController.ParentViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        }

        public void StartCall(string userAlias)
        {
            this.callUserAlias = userAlias;
            startWindowCall();
        }

        public void StartChat(string userAlias)
        {
            this.chatUserAlias = userAlias;
            startChatController(this.chatClient);
        }
        #endregion

        public class BandyerSdkBCXHandleProvider : NSObject, IBCXHandleProvider
        {
            public void Completion(string[] aliases, Action<CXHandle> completion)
            {
                Debug.Print("IBCXHandleProvider Completion " + aliases + " " + completion);

                CXHandle handle = new CXHandle(CXHandleType.Generic, String.Join(", ", aliases));

                completion(handle);
            }

            [return: Release]
            public NSObject Copy(NSZone zone)
            {
                return new BandyerSdkBCXHandleProvider();
            }
        }

        public class BandyerSdkBDKUserInfoFetcher : NSObject, IBDKUserInfoFetcher
        {
            public List<BDKUserInfoDisplayItem> Items { get; set; }

            public BandyerSdkBDKUserInfoFetcher(List<BDKUserInfoDisplayItem> items)
            {
                this.Items = items;
            }

            [return: Release]
            public NSObject Copy(NSZone zone)
            {
                return new BandyerSdkBDKUserInfoFetcher(items: Items);
            }

            public void FetchUsersCompletion(string[] aliases, Action<NSArray<BDKUserInfoDisplayItem>> completion)
            {
                Debug.Print("IBDKUserInfoFetcher FetchUsersCompletion " + aliases + " " + completion);

                var arr = NSArray<BDKUserInfoDisplayItem>.FromNSObjects(Items.ToArray());
                completion(arr);
            }
        }

        void initCallWindow()
        {
            if (callWindow == null)
            {
                callWindow = new BDKCallWindow();
                callWindow.CallDelegate = this;
                var config = new BDKCallViewControllerConfiguration();
                //var url = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"));
                //config.FakeCapturerFileURL = url;
                callWindow.SetConfiguration(config);
            }
        }

        void startWindowCall()
        {
            initCallWindow();
            var callee = new string[] { callUserAlias };
            var intent = BDKMakeCallIntent.IntentWithCallee(callee, BDKCallType.AudioVideoCallType);
            callWindow.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
            {
                Debug.Print("ShouldPresentCallViewControllerWithIntent success " + success);
            });
        }

        void startChatController(IBCHChatClient client)
        {
            if (client != null)
            {
                client.Start(chatUserAlias);
            }
            var intent = BCHOpenChatIntent.OpenChatWith(chatUserAlias);
            var rootVC = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var items = userInfoFetcherItems();
            var userInfoFetcher = new BandyerSdkBDKUserInfoFetcher(items);
            var configuration = new BCHChannelViewControllerConfiguration(audioButton: true, videoButton: true, userInfoFetcher: userInfoFetcher);
            var channelVC = new BCHChannelViewController();
            channelVC.Delegate = this;
            channelVC.Configuration = configuration;
            channelVC.Intent = intent;
            rootVC.PresentViewController(channelVC, true, null);
        }

        List<BDKUserInfoDisplayItem> userInfoFetcherItems()
        {
            var items = new List<BDKUserInfoDisplayItem>();
            var item = new BDKUserInfoDisplayItem("alias");
            item.FirstName = "firstName";
            item.LastName = "lastName";
            item.Email = "email@email.com";
            item.ImageURL = new NSUrl("https://static.bandyer.com/corporate/logos/logo_bandyer_only_name.png");
            items.Add(item);
            return items;
        }

        void handleIncomingCall()
        {
            initCallWindow();
            var config = new BDKCallViewControllerConfiguration();
            //var url = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"));
            //config.FakeCapturerFileURL = url;
            callWindow.SetConfiguration(config);
            var intent = new BDKIncomingCallHandlingIntent();
            callWindow.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
            {
                Debug.Print("ShouldPresentCallViewControllerWithIntent success " + success);
                if (!success)
                {
                    var alert = UIAlertController.Create("Warning", "Another call is already in progress.", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
                }
            });
        }

        #region IBCXCallClientObserver
        [Export("callClient:didReceiveIncomingCall:")]
        public void CallClientDidReceiveIncomingCall(IBCXCallClient client, IBCXCall call)
        {
            Debug.Print("CallClientDidReceiveIncomingCall " + client + " " + call);
            handleIncomingCall();
        }
        [Export("callClientDidPause:")]
        public void CallClientDidPause(IBCXCallClient client)
        {
            Debug.Print("CallClientDidPause " + client);
        }
        [Export("callClientDidResume:")]
        public void CallClientDidResume(IBCXCallClient client)
        {
            Debug.Print("CallClientDidResume " + client);
        }
        [Export("callClientDidStart:")]
        public void CallClientDidStart(IBCXCallClient client)
        {
            Debug.Print("CallClientDidStart " + client);
            CallReadyEvent();
        }
        [Export("callClientDidStartReconnecting:")]
        public void CallClientDidStartReconnecting(IBCXCallClient client)
        {
            Debug.Print("CallClientDidStartReconnecting " + client);
        }
        [Export("callClientDidStop:")]
        public void CallClientDidStop(IBCXCallClient client)
        {
            Debug.Print("CallClientDidStop " + client);
        }
        [Export("callClientWillPause:")]
        public void CallClientWillPause(IBCXCallClient client)
        {
            Debug.Print("CallClientWillPause " + client);
        }
        [Export("callClientWillResume:")]
        public void CallClientWillResume(IBCXCallClient client)
        {
            Debug.Print("CallClientWillResume " + client);
        }
        [Export("callClientWillStart:")]
        public void CallClientWillStart(IBCXCallClient client)
        {
            Debug.Print("CallClientWillStart " + client);
        }
        [Export("callClientWillStop:")]
        public void CallClientWillStop(IBCXCallClient client)
        {
            Debug.Print("CallClientWillStop " + client);
        }
        [Export("callClient:didFailWithError:")]
        public void CallClientDidFailWithError(IBCXCallClient client, NSError error)
        {
            Debug.Print("CallClientDidFailWithError " + client + " " + error);
        }
        #endregion

        #region IBCHChatClientObserver
        [Export("chatClientWillStart:")]
        public void ChatClientWillStart(IBCHChatClient client)
        {
            Debug.Print("ChatClientWillStart " + client);
        }
        [Export("chatClientDidStart:")]
        public void ChatClientDidStart(IBCHChatClient client)
        {
            Debug.Print("ChatClientDidStart " + client);
            this.chatClient = client;
            ChatReadyEvent();
        }
        [Export("chatClientWillPause:")]
        public void ChatClientWillPause(IBCHChatClient client)
        {
            Debug.Print("ChatClientWillPause " + client);
        }
        [Export("chatClientDidPause:")]
        public void ChatClientDidPause(IBCHChatClient client)
        {
            Debug.Print("ChatClientDidPause " + client);
        }
        [Export("chatClientWillStop:")]
        public void ChatClientWillStop(IBCHChatClient client)
        {
            Debug.Print("ChatClientWillStop " + client);
        }
        [Export("chatClientDidStop:")]
        public void ChatClientDidStop(IBCHChatClient client)
        {
            Debug.Print("ChatClientDidStop " + client);
        }
        [Export("chatClientWillResume:")]
        public void ChatClientWillResume(IBCHChatClient client)
        {
            Debug.Print("ChatClientWillResume " + client);
        }
        [Export("chatClientDidResume:")]
        public void ChatClientDidResume(IBCHChatClient client)
        {
            Debug.Print("ChatClientDidResume " + client);
        }
        [Export("chatClient:didFailWithError:")]
        public void ChatClientDidFailWithError(IBCHChatClient client, NSError error)
        {
            Debug.Print("ChatClientDidFailWithError " + client + " " + error);
        }
        #endregion

        #region IBCHChannelViewControllerDelegate
        public void ChannelViewControllerDidFinish(BCHChannelViewController controller)
        {
            Debug.Print("ChannelViewControllerDidFinish " + controller);
            UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);
        }
        public void ChannelViewControllerDidTouchBanner(BCHChannelViewController controller, BDKCallBannerView banner)
        {
            Debug.Print("ChannelViewControllerDidTouchBanner " + controller + " " + banner);
        }
        public void ChannelViewControllerDidTapAudioCallWith(BCHChannelViewController controller, string[] users)
        {
            Debug.Print("ChannelViewControllerDidTapAudioCallWith " + controller + " " + users);
        }
        public void ChannelViewControllerDidTapVideoCallWith(BCHChannelViewController controller, string[] users)
        {
            Debug.Print("ChannelViewControllerDidTapVideoCallWith " + controller + " " + users);
        }
        #endregion

        #region IBDKCallWindowDelegate
        public void CallWindowDidFinish(BDKCallWindow window)
        {
            Debug.Print("CallWindowDidFinish " + window);
            window.DismissCallViewControllerWithCompletion(() => { });
            window.Hidden = true;
        }
        [Export("callWindow:openChatWith:")]
        public void CallWindowOpenChatWith(BDKCallWindow window, BCHOpenChatIntent intent)
        {
            Debug.Print("CallWindowOpenChatWith " + window + " " + intent);
            window.DismissCallViewControllerWithCompletion(() => { });
            window.Hidden = true;
            startChatController(this.chatClient);
        }
        #endregion

        #region IPKPushRegistryDelegate
        public void DidUpdatePushCredentials(PKPushRegistry registry, PKPushCredentials credentials, string type)
        {
            Debug.Print("DidUpdatePushCredentials " + registry + " " + credentials + " " + type);
            var tokenStr = credentials.Bcx_tokenAsString();
            registerTokenToBandyer(tokenStr);
        }
        public void DidReceiveIncomingPush(PKPushRegistry registry, PKPushPayload payload, string type)
        {
            Debug.Print("DidReceiveIncomingPush " + registry + " " + payload + " " + type);
        }
        #endregion

        void registerTokenToBandyer(string token)
        {
            var urlStr = "https://sandbox.bandyer.com/mobile_push_notifications/rest/device";
            var jsonStr = "{" +
                "\"user_alias\":\"client\"" +
                ",\"app_id\":\"" + BandyerSdkiOS.AppId + "\"" +
                ",\"push_token\":\"" + token + "\"" +
                ",\"push_provider\":\"\"" +
                ",\"platform\":\"ios\"" +
                "}";

            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonStr);
                byte[] responseBytes = wc.UploadData(new Uri(urlStr), "POST", dataBytes);
                string responseString = Encoding.UTF8.GetString(responseBytes);

                Debug.Print("UploadData " + responseString);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        #region IBCHMessageNotificationControllerDelegate
        public void DidTouch(BCHMessageNotificationController controller, BCHChatNotification notification)
        {
            Debug.Print("IBCHMessageNotificationControllerDelegate DidTouch " + controller + " " + notification);
            startChatController(null);
        }
        #endregion

        #region IBDKCallBannerControllerDelegate
        public void DidTouch(BDKCallBannerController controller, BDKCallBannerView banner)
        {
            Debug.Print("IBDKCallBannerControllerDelegate DidTouch " + controller + " " + banner);
        }
        [Export("callBannerController:willHide:")]
        public void WillHide(BDKCallBannerController controller, BDKCallBannerView banner)
        {
            Debug.Print("IBDKCallBannerControllerDelegate WillHide " + controller + " " + banner);
        }
        [Export("callBannerController:willShow:")]
        public void WillShow(BDKCallBannerController controller, BDKCallBannerView banner)
        {
            Debug.Print("IBDKCallBannerControllerDelegate WillShow " + controller + " " + banner);
        }
        #endregion
    }
}
