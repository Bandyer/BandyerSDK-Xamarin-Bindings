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
            BandyerSdkAndroid.InitSdk(this);
        }
    }
}
