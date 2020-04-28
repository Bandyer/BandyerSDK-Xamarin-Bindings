using System;
namespace BandyerDemo
{
    public interface IBandyerSdk
    {
        void Init(string userAlias);
        void StartCall(string userAlias);
        void StartChat(string userAlias);
    }
}
