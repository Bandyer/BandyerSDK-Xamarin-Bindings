using System;
using Android.App;
using Android.Runtime;
using Com.Bandyer.Android_sdk;

namespace BandyerDemo.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            String appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";
            BandyerSDK.Builder builder = new BandyerSDK.Builder(this, appId)
                .WithCallEnabled()
                .WithFileSharingEnabled()
                .WithWhiteboardEnabled()
                .WithChatEnabled();
            BandyerSDK.Init(builder);
        }
    }
}
