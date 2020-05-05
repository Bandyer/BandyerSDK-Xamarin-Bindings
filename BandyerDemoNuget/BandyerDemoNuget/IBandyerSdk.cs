using System;
namespace BandyerDemoNuget
{
    public interface IBandyerSdk
    {
        void Init(string userAlias);
        void StartCall(string userAlias);
        void StartChat(string userAlias);
        void StartChatAndCall(string userAlias);
    }
}
