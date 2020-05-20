// Copyright © 2020 Bandyer. All rights reserved.
// See LICENSE for licensing information

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BandyerDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
