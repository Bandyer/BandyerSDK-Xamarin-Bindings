using System;
using Android.App;

namespace BandyerDemo.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public override void OnCreate()
        {
            base.OnCreate();
            String appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";
            new Com.Bandyer.Android_sdk.BandyerSDK.Builder(this, appId);
        }
    }
}
