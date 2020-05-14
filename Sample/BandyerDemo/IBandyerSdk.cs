using System;
namespace BandyerDemo
{
    public interface IBandyerSdk
    {
        event Action<bool> CallStatus;
        event Action<bool> ChatStatus;
        void Init(string userAlias);
        void StartCall(string userAlias);
        void StartChat(string userAlias);
        void OnPageAppearing();
        void OnPageDisappearing();
    }
}
