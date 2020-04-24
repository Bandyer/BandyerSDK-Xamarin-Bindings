using System;
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

        int i = 0;
        public void StartCall(string userAlias)
        {
            i++;
            if (i == 1)
            {
                //BandyerSDK.Instance().CallClient.AddObserver(this, DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Default));
                BandyerSDK.Instance().CallClient.Start(userAlias);
            }
            if (i == 2)
            {
                if (window == null)
                {
                    window = new BDKCallWindow();
                    window.WeakCallDelegate = this;
                    var config = new BDKCallViewControllerConfiguration();
                    var url = new NSUrl(NSBundle.MainBundle.PathForResource("video", "mp4"));
                    config.FakeCapturerFileURL = url;
                    window.SetConfiguration(config);
                }
                var callee = new string[] { userAlias };
                var intent = BDKMakeCallIntent.IntentWithCallee(callee, BDKCallType.AudioVideoCallType);
                window.ShouldPresentCallViewControllerWithIntent(intent, (success) =>
                {
                    Debug.Print("ShouldPresentCallViewControllerWithIntent success " + success);
                });
            }
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
    }
}
