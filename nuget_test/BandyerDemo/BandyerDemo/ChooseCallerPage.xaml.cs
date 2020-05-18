using System;
using System.Collections.Generic;
using BandyerDemo.Models;
using Xamarin.Forms;

namespace BandyerDemo
{
    public partial class ChooseCallerPage : ContentPage
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

        public ChooseCallerPage()
        {
            InitializeComponent();
            userList.ItemsSource = users;
        }

        async void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var obj = e.Item as User;
            if (obj == null)
                return;
            await Navigation.PushAsync(new ChooseCalleePage(obj));
        }
    }
}
