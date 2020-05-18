using System;
using System.Collections.Generic;
using BandyerDemo.Models;
using Xamarin.Forms;

namespace BandyerDemo
{
    public partial class ChooseCalleePage : ContentPage
    {
        private List<User> users = new List<User>() {
            new User()
            {
                Name = "client",
            },
            new User()
            {
                Name = "web",
            },
        };

        public ChooseCalleePage(User user)
        {
            InitializeComponent();
            userList.ItemsSource = users;

            App.BandyerSdk.ChatStatus += ChatStatus;
            App.BandyerSdk.CallStatus += CallStatus;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var userAlias = "client";
            App.BandyerSdk.Init(userAlias);
            App.BandyerSdk.OnPageAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.BandyerSdk.OnPageDisappearing();
        }

        void ChatStatus(bool isReady)
        {
            if (isReady)
            {
                ButtonStartChat.IsEnabled = true;
                ButtonStartChat.BackgroundColor = Color.FromHex("#00ff00");
            }
            else
            {
                ButtonStartChat.IsEnabled = false;
                ButtonStartChat.BackgroundColor = Color.FromHex("#ff0000");
            }
        }

        void CallStatus(bool isReady)
        {
            if (isReady)
            {
                ButtonStartCall.IsEnabled = true;
                ButtonStartCall.BackgroundColor = Color.FromHex("#00ff00");
            }
            else
            {
                ButtonStartCall.IsEnabled = false;
                ButtonStartCall.BackgroundColor = Color.FromHex("#ff0000");
            }
        }

        void Button_StartCall(System.Object sender, System.EventArgs e)
        {
            App.BandyerSdk.StartCall("web");
        }

        void Button_StartChat(System.Object sender, System.EventArgs e)
        {
            App.BandyerSdk.StartChat("web");
        }

        async void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var obj = e.Item as User;
            if (obj == null)
                return;
            var index = users.IndexOf(obj);
            users[index].Selected = !users[index].Selected;
            userList.ItemsSource = null;
            userList.ItemsSource = users;
        }
    }
}
