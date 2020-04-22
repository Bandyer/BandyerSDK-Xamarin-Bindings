using System;
using System.Collections.Generic;
using System.Linq;

using Bandyer;
using Foundation;
using UIKit;

namespace BandyerDemo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var appId = "mAppId_b78542f60f697c8a56a13e579f2e66d0378ba6b3336fa75f961c6efb0e6b";

            var config = new BDKConfig();
            config.Environment = BDKEnvironment.Sandbox;
            config.CallKitEnabled = true;
            BandyerSDK.Instance().InitializeWithApplicationId(appId,config);

            return base.FinishedLaunching(app, options);
        }
    }
}
