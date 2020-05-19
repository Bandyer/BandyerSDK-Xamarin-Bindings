using System;
using System.Collections.Generic;
using BandyerDemo.Models;

namespace BandyerDemo
{
    public interface IBandyerSdk
    {
        event Action<bool> CallStatus;
        event Action<bool> ChatStatus;
        void Init(string userAlias);
        void SetUserDetails(List<User> usersDetails);
        void StartCall(List<string> userAliases);
        void StartChat(string userAlias);
        void OnPageAppearing();
        void OnPageDisappearing();
    }
}
