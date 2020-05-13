using System;
namespace BandyerDemo
{
    public interface IBandyerSdk
    {
        event Action CallReadyEvent;
        event Action ChatReadyEvent;
        void Init(string userAlias);
        void StartCall(string userAlias);
        void StartChat(string userAlias);
        void OnPageAppearing();
        void OnPageDisappearing();
    }
}
