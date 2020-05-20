using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace BandyerDemo.Droid
{
    [Activity(Label = "BandyerDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme.Launcher", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            var intent = new Intent(this, typeof(MainActivity));
            if (Intent.Extras != null)
                intent.PutExtras(Intent.Extras);
            StartActivity(intent);
        }
    }
}
