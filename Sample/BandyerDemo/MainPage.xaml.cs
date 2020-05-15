// Copyright © 2020 Bandyer. All rights reserved.
// See LICENSE for licensing information

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
            bandyerSdk.ChatStatus += ChatStatus;
            bandyerSdk.CallStatus  += CallStatus;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var userAlias = "client";
            bandyerSdk.Init(userAlias);
            bandyerSdk.OnPageAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            bandyerSdk.OnPageDisappearing();
        }

        void ChatStatus(bool isReady)
        {
            ButtonStartChat.IsEnabled = isReady;
        }

        void CallStatus(bool isReady)
        {
            ButtonStartCall.IsEnabled = isReady;
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
