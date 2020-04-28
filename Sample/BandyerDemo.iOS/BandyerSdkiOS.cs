﻿using System;
using System.Diagnostics;
using Bandyer;
using BandyerDemo.iOS;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using PushKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BandyerSdkiOS))]
namespace BandyerDemo.iOS
{
    public class BandyerSdkiOS : NSObject
        , IBandyerSdk
        , IBCXCallClientObserver
    {
        private static BandyerSdkPKPushRegistryDelegate pushDel;
        private BDKCallWindow window = null;
        private string callUserAlias;

        //private BandyerSdkBCXCallClientObserver observer;

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
        }

        public void StartCall(string userAlias)
        {
            this.callUserAlias = userAlias;
            BandyerSDK.Instance().CallClient.AddObserver(this, DispatchQueue.MainQueue);
            BandyerSDK.Instance().CallClient.Start(userAlias);
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
            if (window == null)
            {
                window = new BDKCallWindow();
                //window.WeakCallDelegate = this;
                var config = new BDKCallViewControllerConfiguration();
                var url = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"));
                config.FakeCapturerFileURL = url;
                window.SetConfiguration(config);
            }
            var callee = new string[] { callUserAlias };
            var intent = BDKMakeCallIntent.IntentWithCallee(callee, BDKCallType.AudioVideoCallType);
            window.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
            {
                Debug.Print("ShouldPresentCallViewControllerWithIntent success " + success);
            });
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
    }
}
