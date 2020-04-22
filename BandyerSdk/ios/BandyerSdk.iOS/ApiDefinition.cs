using System;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using Intents;
using ObjCRuntime;
using PushKit;
using UIKit;

namespace Bandyer
{
	// @interface BandyerSDK : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface BandyerSDK
	{
		// @property (readonly, copy, nonatomic) BDKConfig * _Nullable config;
		[NullAllowed, Export("config", ArgumentSemantic.Copy)]
		BDKConfig Config { get; }

		//// @property (readonly, nonatomic, strong) id<BCXCallClient> _Nonnull callClient;
		//[Export("callClient", ArgumentSemantic.Strong)]
		//BCXCallClient CallClient { get; }

		//// @property (readonly, nonatomic, strong) id<BCHChatClient> _Nonnull chatClient;
		//[Export("chatClient", ArgumentSemantic.Strong)]
		//BCHChatClient ChatClient { get; }

		// -(void)initializeWithApplicationId:(NSString * _Nonnull)appId;
		[Export("initializeWithApplicationId:")]
		void InitializeWithApplicationId(string appId);

		// -(void)initializeWithApplicationId:(NSString * _Nonnull)appId config:(BDKConfig * _Nonnull)config;
		[Export("initializeWithApplicationId:config:")]
		void InitializeWithApplicationId(string appId, BDKConfig config);

		// +(instancetype _Nonnull)instance;
		[Static]
		[Export("instance")]
		BandyerSDK Instance();
	}

	// @interface BDKConfig : NSObject <NSCopying>
	[BaseType(typeof(NSObject))]
	interface BDKConfig : INSCopying
	{
		//// @property (copy, nonatomic) id<BDKUserInfoFetcher> _Null_unspecified userInfoFetcher;
		//[Export("userInfoFetcher", ArgumentSemantic.Copy)]
		//BDKUserInfoFetcher UserInfoFetcher { get; set; }

		// @property (copy, nonatomic) BDKEnvironment * _Nonnull environment;
		[Export("environment", ArgumentSemantic.Copy)]
		BDKEnvironment Environment { get; set; }

		// @property (getter = isCallKitEnabled, assign, nonatomic) BOOL callKitEnabled;
		[Export("callKitEnabled")]
		bool CallKitEnabled { [Bind("isCallKitEnabled")] get; set; }

		// @property (copy, nonatomic) NSSet<NSNumber *> * _Nonnull supportedHandleTypes __attribute__((availability(ios, introduced=10.0)));
		[iOS(10, 0)]
		[Export("supportedHandleTypes", ArgumentSemantic.Copy)]
		NSSet<NSNumber> SupportedHandleTypes { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull nativeUILocalizedName __attribute__((availability(ios, introduced=10.0)));
		[iOS(10, 0)]
		[Export("nativeUILocalizedName")]
		string NativeUILocalizedName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable nativeUIRingToneSound __attribute__((availability(ios, introduced=10.0)));
		[iOS(10, 0)]
		[NullAllowed, Export("nativeUIRingToneSound")]
		string NativeUIRingToneSound { get; set; }

		// @property (copy, nonatomic) NSData * _Nullable nativeUITemplateIconImageData __attribute__((availability(ios, introduced=10.0)));
		[iOS(10, 0)]
		[NullAllowed, Export("nativeUITemplateIconImageData", ArgumentSemantic.Copy)]
		NSData NativeUITemplateIconImageData { get; set; }

		//// @property (nonatomic, strong) id<BCXHandleProvider> _Null_unspecified handleProvider __attribute__((availability(ios, introduced=10.0)));
		//[iOS(10, 0)]
		//[Export("handleProvider", ArgumentSemantic.Strong)]
		//BCXHandleProvider HandleProvider { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull notificationPayloadKeyPath;
		[Export("notificationPayloadKeyPath")]
		string NotificationPayloadKeyPath { get; set; }

		[Wrap("WeakPushRegistryDelegate")]
		PKPushRegistryDelegate PushRegistryDelegate { get; set; }

		// @property (nonatomic, strong) id<PKPushRegistryDelegate> _Nonnull pushRegistryDelegate;
		[NullAllowed, Export("pushRegistryDelegate", ArgumentSemantic.Strong)]
		NSObject WeakPushRegistryDelegate { get; set; }

		//// @property (nonatomic, class) BDFDDLogLevel logLevel;
		//[Static]
		//[Export("logLevel", ArgumentSemantic.Assign)]
		//BDFDDLogLevel LogLevel { get; set; }

		// @property (readonly, nonatomic, class) NSInteger logContext;
		[Static]
		[Export("logContext")]
		nint LogContext { get; }

		// @property (readonly, nonatomic, class) NSString * _Nonnull logTag;
		[Static]
		[Export("logTag")]
		string LogTag { get; }

		//// +(void)setLog:(BDFDDLog * _Nullable)log;
		//[Static]
		//[Export("setLog:")]
		//void SetLog([NullAllowed] BDFDDLog log);

		//// +(void)addLogger:(id<BDFDDLogger> _Nonnull)logger;
		//[Static]
		//[Export("addLogger:")]
		//void AddLogger(BDFDDLogger logger);

		//// +(void)removeLogger:(id<BDFDDLogger> _Nonnull)logger;
		//[Static]
		//[Export("removeLogger:")]
		//void RemoveLogger(BDFDDLogger logger);

		// +(instancetype _Nonnull)new;
		[Static]
		[Export("new")]
		BDKConfig New();
	}

	// @interface BDKEnvironment : NSObject <NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKEnvironment : INSCopying
	{
		// @property (readonly, nonatomic, class) BDKEnvironment * _Nonnull production;
		[Static]
		[Export("production")]
		BDKEnvironment Production { get; }

		// @property (readonly, nonatomic, class) BDKEnvironment * _Nonnull sandbox;
		[Static]
		[Export("sandbox")]
		BDKEnvironment Sandbox { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull name;
		[Export("name", ArgumentSemantic.Strong)]
		string Name { get; }
	}
}
