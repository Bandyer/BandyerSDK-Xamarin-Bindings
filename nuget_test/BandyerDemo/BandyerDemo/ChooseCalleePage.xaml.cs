using System;
using System.Collections.Generic;
using System.Linq;
using BandyerDemo.Models;
using Xamarin.Forms;

namespace BandyerDemo
{
    public partial class ChooseCalleePage : ContentPage
    {
        public ChooseCalleePage(User user)
        {
            InitializeComponent();
            userList.ItemsSource = App.Callee;

            App.BandyerSdk.ChatStatus += ChatStatus;
            App.BandyerSdk.CallStatus += CallStatus;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var userAlias = "client";
            App.BandyerSdk.SetUserDetails(App.Callers.Concat(App.Callee).ToList());
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

        async void Button_StartCall(System.Object sender, System.EventArgs e)
        {
            var users = getSelectedUsersNames();
            if (users.Count == 0)
            {
                await DisplayAlert(null, "Select at least 1 user", "OK");
                return;
            }
            App.BandyerSdk.StartCall(users);
        }

        async void Button_StartChat(System.Object sender, System.EventArgs e)
        {
            var users = getSelectedUsersNames();
            if (users.Count == 0)
            {
                await DisplayAlert(null, "Select at least 1 user", "OK");
                return;
            }
            if (users.Count > 1)
            {
                await DisplayAlert(null, "Group chats are not yet supported", "OK");
                return;
            }
            App.BandyerSdk.StartChat(users[0]);
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var obj = e.Item as User;
            if (obj == null)
                return;
            var index = App.Callee.IndexOf(obj);
            App.Callee[index].Selected = !App.Callee[index].Selected;
            userList.ItemsSource = null;
            userList.ItemsSource = App.Callee;
        }

        List<String> getSelectedUsersNames()
        {
            return App.Callee.Where(u => u.Selected).Select(u => u.Alias).ToList();
        }
    }
}
