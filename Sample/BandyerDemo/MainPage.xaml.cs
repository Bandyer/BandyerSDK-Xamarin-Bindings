using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BandyerDemo
{
    public partial class MainPage : ContentPage
    {
        private IBandyerSdk bandyerSdk;

        public MainPage()
        {
            InitializeComponent();
            bandyerSdk = DependencyService.Get<IBandyerSdk>();
            var userAlias = "client";
            bandyerSdk.Init(userAlias);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            bandyerSdk.StartCall("web");
        }
    }
}
