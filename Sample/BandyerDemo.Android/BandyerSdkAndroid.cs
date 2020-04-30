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
using Com.Bandyer.Android_sdk.Client;
using Com.Bandyer.Android_sdk.Intent;
using Com.Bandyer.Android_sdk.Intent.Call;
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
    {
        const string TAG = "BandyerDemo";
        private static Android.App.Application application;

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
        }

        public static void InitSdk(Android.App.Application application)
        {
            var appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

            BandyerSdkAndroid.application = application;

            BandyerSDK.Builder builder = new BandyerSDK.Builder(application, appId)
                .UseSandbox()
                .WithCallEnabled(new BandyerSdkCallNotificationListener())
                .WithFileSharingEnabled()
                .WithWhiteboardEnabled()
                .WithChatEnabled()
                .WithUserDetailsProvider(new BandyerSdkUserDetailsProvider());
            BandyerSDK.Init(builder);
        }

        public void Init(string userAlias)
        {
            BandyerSDKClient.Instance.AddObserver(this);
            BandyerSDKClient.Instance.AddModuleObserver(this);

            BandyerSDKClientOptions options = new BandyerSDKClientOptions.Builder().Build();
            BandyerSDKClient.Instance.Init(userAlias, options);
            BandyerSDKClient.Instance.StartListening();
        }

        public void Dispose()
        {
            BandyerSDKClient.Instance.StopListening();
            BandyerSDKClient.Instance.Dispose();
        }

        public void StartCall(string userAlias)
        {
            BandyerSDKClient.Instance.CallModule.AddCallUIObserver(this);
            BandyerSDKClient.Instance.CallModule.AddCallObserver(this);

            CallCapabilities capabilities = new CallCapabilities()
                        .WithWhiteboard()
                        .WithFileSharing()
                        .WithChat()
                        .WithScreenSharing();

            CallOptions options = new CallOptions()
                        //.WithRecordingEnabled() // if the call started should be recorded
                        //.WithBackCameraAsDefault() // if the call should start with back camera
                        //.WithProximitySensorDisabled() // if the proximity sensor should be disabled during calls
                        ;

            BandyerIntent bandyerCallIntent = new BandyerIntent.Builder()
                    .StartWithAudioVideoCall(application /* context */ )
                /* .startWithAudioUpgradableCall(this) */ // audio call that may upgrade into audio&video call
                /* .startWithAudioCall(this) */  // audio only call
                .With(new List<string>() { "web" })
                //.WithCapabilities(capabilities) // optional
                //.WithOptions(options) // optional
                .Build();

            application.StartActivity(bandyerCallIntent);
        }

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

        public void StartChat(string userAlias)
        {
        }

        public void StartChatAndCall(string userAlias)
        {
        }

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
                    call.Show(application);
                }
                else
                {
                    call.AsNotification().Show(application);
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
