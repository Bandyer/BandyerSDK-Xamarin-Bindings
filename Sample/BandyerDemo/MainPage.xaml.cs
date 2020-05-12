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
        private bool sdkInitialized = false;

        public MainPage()
        {
            InitializeComponent();
            bandyerSdk = DependencyService.Get<IBandyerSdk>();
            bandyerSdk.ChatReadyEvent += ChatReadyEvent;
            bandyerSdk.CallReadyEvent += CallReadyEvent;
        }

        protected override void OnAppearing()
        {
            if (!sdkInitialized)
            {
                sdkInitialized = true;
                var userAlias = "client";
                bandyerSdk.Init(userAlias);
            }
        }

        void ChatReadyEvent()
        {
            ButtonStartChat.IsEnabled = true;
        }

        void CallReadyEvent()
        {
            ButtonStartCall.IsEnabled = true;
        }

        void Button_StartCall(System.Object sender, System.EventArgs e)
        {
            bandyerSdk.StartCall("web");
        }

        void Button_StartChat(System.Object sender, System.EventArgs e)
        {
            bandyerSdk.StartChat("web");
        }
    }
}
