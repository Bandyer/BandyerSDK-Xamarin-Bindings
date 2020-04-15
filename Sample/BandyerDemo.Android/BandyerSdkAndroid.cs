﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Util;
using BandyerDemo.Droid;
using Com.Bandyer.Android_sdk;
using Com.Bandyer.Android_sdk.Client;
using Com.Bandyer.Android_sdk.Intent;
using Com.Bandyer.Android_sdk.Intent.Call;
using Com.Bandyer.Android_sdk.Module;
using Java.Lang;
using Xamarin.Forms;

[assembly: Dependency(typeof(BandyerSdkAndroid))]
namespace BandyerDemo.Droid
{
    public class BandyerSdkAndroid : Java.Lang.Object, IBandyerSdk
        , IBandyerSDKClientObserver, IBandyerModuleObserver
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
                .WithCallEnabled()
                .WithFileSharingEnabled()
                .WithWhiteboardEnabled()
                .WithChatEnabled();
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
    }
}