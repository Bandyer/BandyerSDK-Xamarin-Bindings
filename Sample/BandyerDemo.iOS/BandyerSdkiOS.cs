using System;
using System.Diagnostics;
using Bandyer;
using BandyerDemo.iOS;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using PushKit;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BandyerSdkiOS))]
namespace BandyerDemo.iOS
{
    public class BandyerSdkiOS : NSObject
        , IBandyerSdk
        , IBCXCallClientObserver
        , IBDKCallWindowDelegate
        , IBCHChatClientObserver
        , IBCHChannelViewControllerDelegate
    {
        private static BandyerSdkPKPushRegistryDelegate pushDel;
        private BDKCallWindow callWindow = null;
        private string callUserAlias;
        private string chatUserAlias;
        private string currentUserAlias;

        public static void InitSdk()
        {
            var appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

            BDKConfig.LogLevel = BDFDDLogLevel.Verbose;
            pushDel = new BandyerSdkPKPushRegistryDelegate();
            var config = new BDKConfig();
            config.NotificationPayloadKeyPath = "data";
            config.PushRegistryDelegate = pushDel;
            config.Environment = BDKEnvironment.Sandbox;
            config.CallKitEnabled = true;
            BandyerSDK.Instance().InitializeWithApplicationId(appId, config);
        }

        public void Init(string userAlias)
        {
            this.currentUserAlias = userAlias;
        }

        public void StartCall(string userAlias)
        {
            this.callUserAlias = userAlias;
            BandyerSDK.Instance().CallClient.AddObserver(this, DispatchQueue.MainQueue);
            BandyerSDK.Instance().CallClient.Start(currentUserAlias);
        }

        public void StartChat(string userAlias)
        {
            this.chatUserAlias = userAlias;
            BandyerSDK.Instance().ChatClient.AddObserver(this, DispatchQueue.MainQueue);
            BandyerSDK.Instance().ChatClient.Start(currentUserAlias);
        }

        public class BandyerSdkPKPushRegistryDelegate : PKPushRegistryDelegate
        {
            public override void DidReceiveIncomingPush(PKPushRegistry registry, PKPushPayload payload, string type)
            {
                Debug.Print("DidReceiveIncomingPush");
            }

            public override void DidUpdatePushCredentials(PKPushRegistry registry, PKPushCredentials credentials, string type)
            {
                Debug.Print("DidUpdatePushCredentials");
            }
        }

        void startWindowCall()
        {
            if (callWindow == null)
            {
                callWindow = new BDKCallWindow();
                callWindow.CallDelegate = this;
                var config = new BDKCallViewControllerConfiguration();
                var url = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"));
                config.FakeCapturerFileURL = url;
                callWindow.SetConfiguration(config);
            }
            var callee = new string[] { callUserAlias };
            var intent = BDKMakeCallIntent.IntentWithCallee(callee, BDKCallType.AudioVideoCallType);
            callWindow.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
            {
                Debug.Print("ShouldPresentCallViewControllerWithIntent success " + success);
            });
        }

        void startChatController(IBCHChatClient client)
        {
            client.Start(chatUserAlias);
            var intent = BCHOpenChatIntent.OpenChatWith(chatUserAlias);
            var rootVC = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var configuration = new BCHChannelViewControllerConfiguration(audioButton: true, videoButton: true, userInfoFetcher: null);
            var channelVC = new BCHChannelViewController();
            channelVC.Delegate = this;
            channelVC.Configuration = configuration;
            channelVC.Intent = intent;
            rootVC.PresentViewController(channelVC, true, null);
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
            startWindowCall();
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

        [Export("chatClientWillStart:")]
        public void ChatClientWillStart(IBCHChatClient client)
        {
            Debug.Print("ChatClientWillStart " + client);
        }
        [Export("chatClientDidStart:")]
        public void ChatClientDidStart(IBCHChatClient client)
        {
            Debug.Print("ChatClientDidStart " + client);
            startChatController(client);
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

        public void CallWindowDidFinish(BDKCallWindow window)
        {
            Debug.Print("CallWindowDidFinish " + window);
            window.DismissCallViewControllerWithCompletion(() => { });
            window.Hidden = true;
        }
    }
}
