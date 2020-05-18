using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BandyerDemo
{
    public partial class App : Application
    {
        public static IBandyerSdk BandyerSdk { get; private set; }

        public App()
        {
            InitializeComponent();
            BandyerSdk = DependencyService.Get<IBandyerSdk>();

            var navPage = new NavigationPage(new ChooseCallerPage());
            navPage.BarTextColor = Color.White;
            navPage.BarBackgroundColor = Color.FromHex("#004c8c");
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
