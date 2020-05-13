using System;
using System.Collections;
using System.Collections.Generic;
using Android.App;
using Android.Util;
using BandyerDemo.Droid;
using Com.Bandyer.Android_sdk;
using Com.Bandyer.Android_sdk.Call;
using Com.Bandyer.Android_sdk.Call.Model;
using Com.Bandyer.Android_sdk.Call.Notification;
using Com.Bandyer.Android_sdk.Chat;
using Com.Bandyer.Android_sdk.Client;
using Com.Bandyer.Android_sdk.Intent;
using Com.Bandyer.Android_sdk.Intent.Call;
using Com.Bandyer.Android_sdk.Intent.Chat;
using Com.Bandyer.Android_sdk.Module;
using Com.Bandyer.Android_sdk.Utils.Provider;
using Java.Lang;
using Xamarin.Forms;

[assembly: Dependency(typeof(BandyerSdkAndroid))]
namespace BandyerDemo.Droid
{
    public class BandyerSdkAndroid : Java.Lang.Object
        , IBandyerSdk
        , IBandyerSDKClientObserver
        , IBandyerModuleObserver
        , ICallUIObserver
        , ICallObserver
        , IChatUIObserver
        , IChatObserver
    {
        public const string AppId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

        const string TAG = "BandyerDemo";
        public static Android.App.Activity MainActivity;
        private bool isChatModuleConnected;
        private bool isCallModuleConnected;
        private bool isSdkInitialized = false;

        #region IBandyerSDKClientObserver
        public void OnClientError(Throwable throwable)
        {
            Log.Debug(TAG, "OnClientError " + throwable);
        }

        public void OnClientReady()
        {
            Log.Debug(TAG, "OnClientReady");
        }

        public void OnClientStatusChange(BandyerSDKClientState state)
        {
            Log.Debug(TAG, "OnClientStatusChange " + state);
        }

        public void OnClientStopped()
        {
            Log.Debug(TAG, "OnClientStopped");
        }
        #endregion

        #region IBandyerModuleObserver
        public void OnModuleFailed(IBandyerModule module, Throwable throwable)
        {
            Log.Debug(TAG, "OnModuleFailed " + module + " " + throwable);
        }

        public void OnModulePaused(IBandyerModule module)
        {
            Log.Debug(TAG, "OnModulePaused " + module);
        }

        public void OnModuleReady(IBandyerModule module)
        {
            Log.Debug(TAG, "OnModuleReady " + module);
        }

        public void OnModuleStatusChanged(IBandyerModule module, BandyerModuleStatus moduleStatus)
        {
            Log.Debug(TAG, "OnModuleStatusChanged " + module + " " + moduleStatus);
            if (module.Name == "ChatModule" && module.Status == BandyerModuleStatus.Ready)
            {
                isChatModuleConnected = true;
                ChatReadyEvent();
            }
            if (module.Name == "CallModule" && module.Status == BandyerModuleStatus.Ready)
            {
                isCallModuleConnected = true;
                CallReadyEvent();
            }
        }
        #endregion

        public void Dispose()
        {
            BandyerSDKClient.Instance.StopListening();
            BandyerSDKClient.Instance.Dispose();
        }

        public static void InitSdk(Android.App.Application application)
        {
            BandyerSDK.Builder builder = new BandyerSDK.Builder(application, AppId)
                .UseSandbox()
                .WithCallEnabled(new BandyerSdkCallNotificationListener())
                .WithFileSharingEnabled()
                .WithWhiteboardEnabled()
                .WithChatEnabled()
                .WithUserDetailsProvider(new BandyerSdkUserDetailsProvider());
            BandyerSDK.Init(builder);
        }

        #region IBandyerSdk
        public event Action CallReadyEvent;
        public event Action ChatReadyEvent;

        public void Init(string userAlias)
        {
            if (!isSdkInitialized)
            {
                isSdkInitialized = true;
                BandyerSDKClient.Instance.AddObserver(this);
                BandyerSDKClient.Instance.AddModuleObserver(this);

                BandyerSDKClientOptions options = new BandyerSDKClientOptions.Builder().Build();
                BandyerSDKClient.Instance.Init(userAlias, options);
                BandyerSDKClient.Instance.StartListening();

                BandyerSDKClient.Instance.ChatModule.AddChatUIObserver(this);
                BandyerSDKClient.Instance.ChatModule.AddChatObserver(this);

                BandyerSDKClient.Instance.CallModule.AddCallUIObserver(this);
                BandyerSDKClient.Instance.CallModule.AddCallObserver(this);
            }
        }

        public void StartCall(string userAlias)
        {
            CallCapabilities capabilities = new CallCapabilities();
            capabilities.WithWhiteboard();
            capabilities.WithFileSharing();
            capabilities.WithChat();
            capabilities.WithScreenSharing();

            CallOptions options = new CallOptions();
            options.WithRecordingEnabled(); // if the call started should be recorded
            options.WithBackCameraAsDefault(); // if the call should start with back camera
            options.WithProximitySensorDisabled(); // if the proximity sensor should be disabled during calls

            BandyerIntent.Builder builder = new BandyerIntent.Builder();
            CallIntentBuilder callIntentBuilder = builder.StartWithAudioVideoCall(MainActivity.Application /* context */ );
            //builder.StartWithAudioUpgradableCall(application); // audio call that may upgrade into audio&video call
            //builder.StartWithAudioCall(application);  // audio only call
            CallIntentOptions callIntentOptions =  callIntentBuilder.With(new List<string>() { "web" });
            callIntentOptions.WithCapabilities(capabilities); // optional
            callIntentOptions.WithOptions(options); // optional
            BandyerIntent bandyerCallIntent = callIntentOptions.Build();

            MainActivity.StartActivity(bandyerCallIntent);
        }

        public void StartChat(string userAlias)
        {
            CallCapabilities capabilities = new CallCapabilities();
            capabilities.WithWhiteboard();
            capabilities.WithFileSharing();
            capabilities.WithChat();
            capabilities.WithScreenSharing();

            CallOptions options = new CallOptions();
            options.WithRecordingEnabled(); // if the call started should be recorded
            options.WithBackCameraAsDefault(); // if the call should start with back camera
            options.WithProximitySensorDisabled(); // if the proximity sensor should be disabled during calls

            BandyerIntent.Builder builder = new BandyerIntent.Builder();
            ChatIntentBuilder chatIntentBuilder = builder.StartWithChat(MainActivity.Application /* context */ );
            ChatIntentOptions chatIntentOptions = chatIntentBuilder.With("web");
            chatIntentOptions.WithAudioCallCapability(capabilities, options); // optional
            chatIntentOptions.WithAudioUpgradableCallCapability(capabilities, options); // optionally upgradable to audio video call
            chatIntentOptions.WithAudioVideoCallCapability(capabilities, options); // optional
            BandyerIntent bandyerChatIntent = chatIntentOptions.Build();

            MainActivity.StartActivity(bandyerChatIntent);
        }

        public void OnPageAppearing()
        {
        }

        public void OnPageDisappearing()
        {
        }
        #endregion

        #region ICallUIObserver
        public void OnActivityDestroyed(ICall call, Java.Lang.Ref.WeakReference activity)
        {
            Log.Debug(TAG, "onCallActivityDestroyed: "
               + call.CallInfo.Caller + ", "
               + System.String.Join(", ", call.CallInfo.Callees));
        }

        public void OnActivityError(ICall call, Java.Lang.Ref.WeakReference activity, CallException error)
        {
            Log.Debug(TAG, "onCallActivityDestroyed: "
              + call.CallInfo.Caller + ", "
              + System.String.Join(", ", call.CallInfo.Callees)
              + "\n"
              + "exception: " + error.Message);
        }

        public void OnActivityStarted(ICall call, Java.Lang.Ref.WeakReference activity)
        {
            Log.Debug(TAG, "onCallActivityStarted: "
               + call.CallInfo.Caller + ", "
               + System.String.Join(", ", call.CallInfo.Callees));
        }
        #endregion

        #region ICallObserver
        public void OnCallCreated(ICall call)
        {
            Log.Debug(TAG, "onCallCreated: "
               + call.CallInfo.Caller + ", "
               + System.String.Join(", ", call.CallInfo.Callees));
        }

        public void OnCallEnded(ICall call)
        {
            Log.Debug(TAG, "onCallEnded: "
               + call.CallInfo.Caller + ", "
               + System.String.Join(", ", call.CallInfo.Callees));
        }

        public void OnCallEndedWithError(ICall call, CallException callException)
        {
            Log.Debug(TAG, "onCallEndedWithError: "
              + call.CallInfo.Caller + ", "
              + System.String.Join(", ", call.CallInfo.Callees)
              + "\n"
              + "exception: " + callException.Message);
        }

        public void OnCallStarted(ICall call)
        {
            Log.Debug(TAG, "onCallStarted: "
               + call.CallInfo.Caller + ", "
               + System.String.Join(", ", call.CallInfo.Callees));
        }
        #endregion

        #region IChatUIObserver
        public void OnActivityDestroyed(IChat chat, Java.Lang.Ref.WeakReference activity)
        {
            Log.Debug(TAG, "onChatActivityDestroyed");
        }

        public void OnActivityError(IChat chat, Java.Lang.Ref.WeakReference activity, ChatException error)
        {
            Log.Debug(TAG, "onChatActivityError " + error.Message);
        }

        public void OnActivityStarted(IChat chat, Java.Lang.Ref.WeakReference activity)
        {
            Log.Debug(TAG, "onChatActivityStarted");
        }
        #endregion

        #region IChatObserver
        public void OnChatEnded()
        {
            Log.Debug(TAG, "OnChatEnded");
        }

        public void OnChatEndedWithError(ChatException chatException)
        {
            Log.Debug(TAG, "OnChatEndedWithError " + chatException.Message);
        }

        public void OnChatStarted()
        {
            Log.Debug(TAG, "OnChatStarted");
        }
        #endregion

        class BandyerSdkCallNotificationListener : Java.Lang.Object
            , ICallNotificationListener
        {
            public void OnCreateNotification(ICallInfo callInfo, CallNotificationType type, ICallNotificationStyle notificationStyle)
            {
                notificationStyle.SetNotificationColor(Android.Graphics.Color.Red);
            }

            public void OnIncomingCall(IIncomingCall call, bool isDnd, bool isScreenLocked)
            {
                call.WithCapabilities(GetDefaultCallCapabilities());
                call.WithOptions(GetDefaultIncomingCallOptions());
                if (!isDnd || isScreenLocked)
                {
                    call.Show(MainActivity);
                }
                else
                {
                    call.AsNotification().Show(MainActivity);
                }
            }

            private CallCapabilities GetDefaultCallCapabilities()
            {
                return new CallCapabilities()
                        .WithChat()
                        .WithWhiteboard()
                        .WithScreenSharing()
                        .WithFileSharing();
            }
            private IncomingCallOptions GetDefaultIncomingCallOptions()
            {
                return new IncomingCallOptions();
            }
        }

        class BandyerSdkUserDetailsProvider : Java.Lang.Object
            , IUserDetailsProvider
        {
            public void OnUserDetailsRequested(IList<string> userAliases, IOnUserDetailsListener onUserDetailsListener)
            {
                Java.Util.ArrayList details = new Java.Util.ArrayList();
                foreach (string userAlias in userAliases)
                {
                    details.Add(new UserDetails.Builder(userAlias)
                      .WithNickName("nickname")
                      .WithFirstName("name")
                      .WithLastName("last name")
                      .WithEmail("email@email.com")
                      .WithImageUri(Android.Net.Uri.Parse("https://static.bandyer.com/corporate/logos/logo_bandyer_only_name.png"))
                      .Build());
                }
                onUserDetailsListener.Provide(details);
            }
        }

    }
}
