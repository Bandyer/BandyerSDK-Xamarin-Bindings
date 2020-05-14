using System;
using System.Net;
using System.Text;
using Android.App;
using Android.Util;
using Com.Bandyer.Android_sdk.Client;
using Firebase.Messaging;

namespace BandyerDemo.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class BandyerDemoFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "BandyerDemoFirebaseMessagingService";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Log.Debug(TAG, "OnNewToken " + token);

            RegisterTokenToBandyer(token);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
            Log.Debug(TAG, "OnMessageReceived " + remoteMessage);

            BandyerSDKClient.Instance.HandleNotification(ApplicationContext, remoteMessage.Data["message"]);
        }

        public static void RegisterTokenToBandyer(string token)
        {
            var urlStr = "https://sandbox.bandyer.com/mobile_push_notifications/rest/device";
            var jsonStr = "{" +
                "\"user_alias\":\"client\"" +
                ",\"app_id\":\"" + BandyerSdkAndroid.AppId + "\"" +
                ",\"push_token\":\"" + token + "\"" +
                ",\"push_provider\":\"firebase\"" +
                ",\"platform\":\"android\"" +
                "}";

            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonStr);
                byte[] responseBytes = wc.UploadData(new Uri(urlStr), "POST", dataBytes);
                string responseString = Encoding.UTF8.GetString(responseBytes);

                Log.Debug(TAG, "UploadData " + responseString);
            }
            catch (Exception e)
            {
                Log.Debug(TAG, e.ToString());
            }
        }
    }
}
