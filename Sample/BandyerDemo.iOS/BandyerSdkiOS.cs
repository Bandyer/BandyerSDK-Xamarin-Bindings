using System;
using System.Diagnostics;
using Bandyer;
using BandyerDemo.iOS;
using CoreFoundation;
using Foundation;
using PushKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BandyerSdkiOS))]
namespace BandyerDemo.iOS
{
    public class BandyerSdkiOS : NSObject
        , IBandyerSdk
    {
        private static BandyerSdkPKPushRegistryDelegate pushDel;
        //private BandyerSdkBCXCallClientObserver observer;

        public static void InitSdk()
        {
            var appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

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
            //observer = new BandyerSdkBCXCallClientObserver();
            //BandyerSDK.Instance().CallClient.AddObserver(observer, DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Default));
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

        public class BandyerSdkBCXCallClientObserver : BCXCallClientObserver
        {
            public override void CallClientDidPause(BCXCallClient client)
            {
                base.CallClientDidPause(client);
                Debug.Print("CallClientDidPause " + client);
            }
            public override void CallClientDidResume(BCXCallClient client)
            {
                base.CallClientDidResume(client);
                Debug.Print("CallClientDidResume " + client);
            }
            public override void CallClientDidStart(BCXCallClient client)
            {
                base.CallClientDidStart(client);
                Debug.Print("CallClientDidStart " + client);
            }
            public override void CallClientDidStartReconnecting(BCXCallClient client)
            {
                base.CallClientDidStartReconnecting(client);
                Debug.Print("CallClientDidStartReconnecting " + client);
            }
            public override void CallClientDidStop(BCXCallClient client)
            {
                base.CallClientDidStop(client);
                Debug.Print("CallClientDidStop " + client);
            }
            public override void CallClientWillPause(BCXCallClient client)
            {
                base.CallClientWillPause(client);
                Debug.Print("CallClientWillPause " + client);
            }
            public override void CallClientWillResume(BCXCallClient client)
            {
                base.CallClientWillResume(client);
                Debug.Print("CallClientWillResume " + client);
            }
            public override void CallClientWillStart(BCXCallClient client)
            {
                base.CallClientWillStart(client);
                Debug.Print("CallClientWillStart " + client);
            }
            public override void CallClientWillStop(BCXCallClient client)
            {
                base.CallClientWillStop(client);
                Debug.Print("CallClientWillStop " + client);
            }
        }
    }
}
