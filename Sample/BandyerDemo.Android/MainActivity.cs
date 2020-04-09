using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Bandyer.Android_sdk.Client;

namespace BandyerDemo.Droid
{
    [Activity(Label = "BandyerDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            initSdk();
        }
        protected override void OnDestroy()
        {
            BandyerSDKClient.Instance.StopListening();
            BandyerSDKClient.Instance.Dispose();

            base.OnDestroy();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        void initSdk()
        {
            String userAlias = "userAlias";
            BandyerSDKClientOptions options = new BandyerSDKClientOptions.Builder().Build();
            BandyerSDKClient.Instance.Init(userAlias, options);
            BandyerSDKClient.Instance.StartListening();
        }
    }
}