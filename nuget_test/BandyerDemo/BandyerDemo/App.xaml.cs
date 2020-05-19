using System;
using System.Collections.Generic;
using BandyerDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BandyerDemo
{
    public partial class App : Application
    {
        public static IBandyerSdk BandyerSdk { get; private set; }

        public static List<User> Callers = new List<User>() {
            new User()
            {
                Alias = "client",
                NickName = "ClientUser",
                FirstName = "John",
                LastName = "Liu",
                Email = "client@client.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_0.jpg",
            },
            new User()
            {
                Alias = "web",
                NickName = "WebUser",
                FirstName = "Jack",
                LastName = "Beck",
                Email = "web@web.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_1.jpg",
            },
        };
        public static List<User> Callee = new List<User>() {
            new User()
            {
                Alias = "client2",
                NickName = "Client2User",
                FirstName = "Mark",
                LastName = "Mendoza",
                Email = "client2@client.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_2.jpg",
            },
            new User()
            {
                Alias = "client3",
                NickName = "Client3User",
                FirstName = "Paul",
                LastName = "Milner",
                Email = "client3@client.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_3.jpg",
            },
            new User()
            {
                Alias = "web2",
                NickName = "Web2User",
                FirstName = "Herbert",
                LastName = "Sanchez",
                Email = "web2@web.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_4.jpg",
            },
            new User()
            {
                Alias = "web3",
                NickName = "Web3User",
                FirstName = "Phil",
                LastName = "Wiley",
                Email = "web3@web.com",
                ImageUri = "https://github.com/Bandyer/Bandyer-iOS-SDK-Samples/raw/master/Basic-Example/BasicExample/Resources/Men/man_5.jpg",
            },
        };

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
