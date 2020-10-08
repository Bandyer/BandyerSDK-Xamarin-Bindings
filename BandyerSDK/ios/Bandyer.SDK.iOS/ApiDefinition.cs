// Copyright © 2020 Bandyer. All rights reserved.
// See LICENSE for licensing information

using System;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using Intents;
using ObjCRuntime;
using PushKit;
using UIKit;
using Bandyer;
using CallKit;

namespace Bandyer
{
	#region Generated interfaces Forward declaration
	
	interface IBCHChannelViewControllerDelegate { }
	
	interface IBCHMessageNotificationControllerDelegate { }

	interface IBCHChatClient { }
	
	interface IBCHChatClientObserver { }

	interface IBDKUserInfoFetcher { }
	
	interface IBDKCallBannerControllerDelegate { }

	interface IBDKCallViewControllerDelegate { }
	
	interface IBDKIntent { }
	
	interface IBDKCallWindowDelegate { }

	interface IBDKInAppNotificationsCoordinator { }
	
	interface IBDKInAppChatNotificationTouchListener { }

	interface IBDKInAppFileShareNotificationTouchListener { }

	interface IBCXUser { }
	
	interface IBCXCallRegistry { }
	
	interface IBCXCallRegistryObserver { }
	
	interface IBCXCallClient { }
	
	interface IBCXCallClientObserver { }

	interface IBCXCallParticipantsObserver { }

	interface IBCXCall { }
	
	interface IBCXCallParticipants { }
	
	interface IBCXCallParticipant { }
	
	interface IBCXCallObserver { }

	interface IBCXHandleProvider { }
	
	#endregion
	
	// @interface BandyerSDK : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BandyerSDK
	{
		// @property (readonly, nonatomic, strong) id<BDKInAppNotificationsCoordinator> _Nullable notificationsCoordinator;
		[NullAllowed, Export ("notificationsCoordinator", ArgumentSemantic.Strong)]
		IBDKInAppNotificationsCoordinator NotificationsCoordinator { get; }

		// @property (readonly, copy, nonatomic) BDKConfig * _Nullable config;
		[NullAllowed, Export ("config", ArgumentSemantic.Copy)]
		BDKConfig Config { get; }

		// @property (readonly, nonatomic, strong) id<BCXCallClient> _Nonnull callClient;
		[Export ("callClient", ArgumentSemantic.Strong)]
		IBCXCallClient CallClient { get; }

		// @property (readonly, nonatomic, strong) id<BCXCallRegistry> _Nonnull callRegistry;
		[Export ("callRegistry", ArgumentSemantic.Strong)]
		IBCXCallRegistry CallRegistry { get; }

		// @property (readonly, nonatomic, strong) id<BCHChatClient> _Nonnull chatClient;
		[Export ("chatClient", ArgumentSemantic.Strong)]
		IBCHChatClient ChatClient { get; }

		// -(void)initializeWithApplicationId:(NSString * _Nonnull)appId;
		[Export ("initializeWithApplicationId:")]
		void InitializeWithApplicationId (string appId);

		// -(void)initializeWithApplicationId:(NSString * _Nonnull)appId config:(BDKConfig * _Nonnull)config;
		[Export ("initializeWithApplicationId:config:")]
		void InitializeWithApplicationId (string appId, BDKConfig config);

		// +(instancetype _Nonnull)instance;
		[Static]
		[Export ("instance")]
		BandyerSDK Instance ();
	}

	// @protocol BDKIntent <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BDKIntent
	{
		// @required @property (readonly, copy, nonatomic) NSUUID * _Nonnull UUID;
		[Abstract]
		[Export ("UUID", ArgumentSemantic.Copy)]
		NSUuid UUID { get; }
	}

	// @interface BDKMakeCallIntent : NSObject <BDKIntent>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKMakeCallIntent : BDKIntent
	{
		// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull callee;
		[Export ("callee", ArgumentSemantic.Copy)]
		string[] Callee { get; }

		// @property (readonly, getter = shouldRecord, assign, nonatomic) BOOL record;
		[Export ("record")]
		bool Record { [Bind ("shouldRecord")] get; }

		// @property (readonly, assign, nonatomic) NSUInteger maximumDuration;
		[Export ("maximumDuration")]
		nuint MaximumDuration { get; }

		// @property (readonly, assign, nonatomic) BDKCallType callType;
		[Export ("callType", ArgumentSemantic.Assign)]
		BDKCallType CallType { get; }

		// +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee;
		[Static]
		[Export ("intentWithCallee:")]
		BDKMakeCallIntent IntentWithCallee (string[] callee);

		// +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type;
		[Static]
		[Export ("intentWithCallee:type:")]
		BDKMakeCallIntent IntentWithCallee (string[] callee, BDKCallType type);

		// +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type record:(BOOL)record;
		[Static]
		[Export ("intentWithCallee:type:record:")]
		BDKMakeCallIntent IntentWithCallee (string[] callee, BDKCallType type, bool record);

		// +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type maximumDuration:(NSUInteger)duration;
		[Static]
		[Export ("intentWithCallee:type:maximumDuration:")]
		BDKMakeCallIntent IntentWithCallee (string[] callee, BDKCallType type, nuint duration);

		// +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type record:(BOOL)record maximumDuration:(NSUInteger)duration;
		[Static]
		[Export ("intentWithCallee:type:record:maximumDuration:")]
		BDKMakeCallIntent IntentWithCallee (string[] callee, BDKCallType type, bool record, nuint duration);
	}

	// @interface BDKJoinURLIntent : NSObject <BDKIntent>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKJoinURLIntent : BDKIntent
	{
		// @property (readonly, copy, nonatomic) NSURL * _Nonnull url;
		[Export ("url", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		// +(instancetype _Nonnull)intentWithURL:(NSURL * _Nonnull)url;
		[Static]
		[Export ("intentWithURL:")]
		BDKJoinURLIntent IntentWithURL (NSUrl url);
	}

	// @interface BDKIncomingCallHandlingIntent : NSObject <BDKIntent>
	[BaseType (typeof(NSObject))]
	interface BDKIncomingCallHandlingIntent : BDKIntent
	{
	}

	// @protocol BDKCallViewControllerDelegate <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKCallViewControllerDelegate
	{
		// @required -(void)callViewControllerDidFinish:(BDKCallViewController * _Nonnull)controller;
		[Abstract]
		[Export ("callViewControllerDidFinish:")]
		void CallViewControllerDidFinish (BDKCallViewController controller);

		// @required -(void)callViewControllerDidPressBack:(BDKCallViewController * _Nonnull)controller;
		[Abstract]
		[Export ("callViewControllerDidPressBack:")]
		void CallViewControllerDidPressBack (BDKCallViewController controller);

		// @required -(void)callViewController:(BDKCallViewController * _Nonnull)controller openChatWith:(NSString * _Nonnull)participantId;
		[Abstract]
		[Export ("callViewController:openChatWith:")]
		void CallViewControllerOpenChatWithParticipant (BDKCallViewController controller, string participantId);
	}

	// @interface BDKCallViewController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface BDKCallViewController
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		IBDKCallViewControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<BDKCallViewControllerDelegate> _Nullable delegate __attribute__((iboutlet));
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)setConfiguration:(BDKCallViewControllerConfiguration * _Nonnull)configuration;
		[Export ("setConfiguration:")]
		void SetConfiguration (BDKCallViewControllerConfiguration configuration);

		// -(void)handleIntent:(id<BDKIntent> _Nonnull)intent __attribute__((swift_name("handle(intent:)")));
		[Export ("handleIntent:")]
		void HandleIntent (IBDKIntent intent);

		// -(void)handleINStartVideoCallIntent:(INStartVideoCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=10.0, deprecated=13.0))) __attribute__((swift_name("handle(startVideoCallIntent:)")));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Deprecated (PlatformName.iOS, 13, 0, message: "handleINStartVideoCallIntent: is deprecated. Please use handleINStartCallIntent: instead")]
		[Export ("handleINStartVideoCallIntent:")]
		void HandleINStartVideoCallIntent (INStartVideoCallIntent intent);

		// -(void)handleINStartCallIntent:(INStartCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=13.0))) __attribute__((swift_name("handle(startCallIntent:)")));
		[Introduced (PlatformName.iOS, 13, 0)]
		[Export ("handleINStartCallIntent:")]
		void HandleINStartCallIntent (INStartCallIntent intent);
	}

	// @interface BDKCallViewControllerConfiguration : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	interface BDKCallViewControllerConfiguration : INSCopying
	{
		// @property (copy, nonatomic, null_resettable) id<BDKUserInfoFetcher> _Null_unspecified userInfoFetcher;
		[NullAllowed, Export ("userInfoFetcher", ArgumentSemantic.Copy)]
		IBDKUserInfoFetcher UserInfoFetcher { get; set; }

		// @property (copy, nonatomic, null_resettable) NSFormatter * _Null_unspecified callInfoTitleFormatter;
		[NullAllowed, Export ("callInfoTitleFormatter", ArgumentSemantic.Copy)]
		NSFormatter CallInfoTitleFormatter { get; set; }

		// @property (copy, nonatomic) NSURL * _Nullable fakeCapturerFileURL;
		[NullAllowed, Export ("fakeCapturerFileURL", ArgumentSemantic.Copy)]
		NSUrl FakeCapturerFileURL { get; set; }
	}

	// @protocol BDKUserInfoFetcher <NSObject, NSCopying>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKUserInfoFetcher : INSCopying
	{
		// @required -(void)fetchUsers:(NSArray<NSString *> * _Nonnull)aliases completion:(void (^ _Nonnull)(NSArray<BDKUserInfoDisplayItem *> * _Nullable))completion;
		[Abstract]
		[Export ("fetchUsers:completion:")]
        void FetchUsersCompletion(string[] aliases, Action<NSArray<BDKUserInfoDisplayItem>> completion);
	}

	// @interface BDKUserInfoDisplayItem : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKUserInfoDisplayItem : INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull alias;
		[Export ("alias")]
		string Alias { get; }

		// @property (copy, nonatomic) NSString * _Nullable firstName;
		[NullAllowed, Export ("firstName")]
		string FirstName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable lastName;
		[NullAllowed, Export ("lastName")]
		string LastName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export ("email")]
		string Email { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable nickname;
		[NullAllowed, Export ("nickname")]
		string Nickname { get; set; }

		// @property (copy, nonatomic) NSURL * _Nullable imageURL;
		[NullAllowed, Export ("imageURL", ArgumentSemantic.Copy)]
		NSUrl ImageURL { get; set; }

		// @property (readonly, copy, nonatomic) UIImage * _Nullable image;
		[NullAllowed, Export ("image", ArgumentSemantic.Copy)]
		UIImage Image { get; }

		// -(instancetype _Nonnull)initWithAlias:(NSString * _Nonnull)alias;
		[Export ("initWithAlias:")]
		IntPtr Constructor (string alias);
	}

	// @protocol BCXCallClientObserver <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BCXCallClientObserver
	{
		// @optional -(void)callClient:(id<BCXCallClient> _Nonnull)client didReceiveIncomingCall:(id<BCXCall> _Nonnull)call;
		[Export ("callClient:didReceiveIncomingCall:")]
        void CallClientDidReceiveIncomingCall(IBCXCallClient client, IBCXCall call);

		// @optional -(void)callClientWillStart:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientWillStart:")]
        void CallClientWillStart(IBCXCallClient client);

		// @optional -(void)callClientDidStart:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientDidStart:")]
        void CallClientDidStart(IBCXCallClient client);

		// @optional -(void)callClientDidStartReconnecting:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientDidStartReconnecting:")]
        void CallClientDidStartReconnecting(IBCXCallClient client);

		// @optional -(void)callClientWillPause:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientWillPause:")]
        void CallClientWillPause(IBCXCallClient client);

		// @optional -(void)callClientDidPause:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientDidPause:")]
        void CallClientDidPause(IBCXCallClient client);

		// @optional -(void)callClientWillStop:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientWillStop:")]
        void CallClientWillStop(IBCXCallClient client);

		// @optional -(void)callClientDidStop:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientDidStop:")]
        void CallClientDidStop(IBCXCallClient client);

		// @optional -(void)callClientWillResume:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientWillResume:")]
        void CallClientWillResume(IBCXCallClient client);

		// @optional -(void)callClientDidResume:(id<BCXCallClient> _Nonnull)client;
		[Export ("callClientDidResume:")]
        void CallClientDidResume(IBCXCallClient client);

		// @optional -(void)callClient:(id<BCXCallClient> _Nonnull)client didFailWithError:(NSError * _Nonnull)error;
		[Export ("callClient:didFailWithError:")]
        void CallClientDidFailWithError(IBCXCallClient client, NSError error);
	}

	// @protocol BCXCallClient <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCXCallClient
	{
		// @required @property (readonly, nonatomic, strong) id<BCXUser> _Nullable user;
		[Abstract]
		[NullAllowed, Export ("user", ArgumentSemantic.Strong)]
		IBCXUser User { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallClientState state;
		[Abstract]
		[Export ("state", ArgumentSemantic.Assign)]
		BCXCallClientState State { get; }

		// @required -(BOOL)isStopped;
		[Abstract]
		[Export ("isStopped")]
		bool IsStopped { get; }

		// @required -(BOOL)isStarting;
		[Abstract]
		[Export ("isStarting")]
		bool IsStarting { get; }

		// @required -(BOOL)isRunning;
		[Abstract]
		[Export ("isRunning")]
		bool IsRunning { get; }

		// @required -(BOOL)isPaused;
		[Abstract]
		[Export ("isPaused")]
		bool IsPaused { get; }

		// @required -(BOOL)isResuming;
		[Abstract]
		[Export ("isResuming")]
		bool IsResuming { get; }

		// @required -(BOOL)isReconnecting;
		[Abstract]
		[Export ("isReconnecting")]
		bool IsReconnecting { get; }

		// @required -(void)addObserver:(id<BCXCallClientObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
		[Abstract]
		[Export ("addObserver:")]
        void AddObserver(IBCXCallClientObserver observer);

		// @required -(void)addObserver:(id<BCXCallClientObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
		[Abstract]
		[Export ("addObserver:queue:")]
        void AddObserver(IBCXCallClientObserver observer, [NullAllowed] DispatchQueue queue);

		// @required -(void)removeObserver:(id<BCXCallClientObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
		[Abstract]
		[Export ("removeObserver:")]
        void RemoveObserver(IBCXCallClientObserver observer);

		// @required -(void)start:(NSString * _Nonnull)userId;
		[Abstract]
		[Export ("start:")]
		void Start (string userId);

		// @required -(void)resume;
		[Abstract]
		[Export ("resume")]
		void Resume ();

		// @required -(void)pause;
		[Abstract]
		[Export ("pause")]
		void Pause ();

		// @required -(void)stop;
		[Abstract]
		[Export ("stop")]
		void Stop ();

		// @required -(void)verifiedUser:(BOOL)verified forCall:(id<BCXCall> _Nonnull)call completion:(void (^ _Nullable)(NSError * _Nullable))completion;
		[Abstract]
		[Export ("verifiedUser:forCall:completion:")]
		void VerifiedUser (bool verified, IBCXCall call, [NullAllowed] Action<NSError> completion);
	}

	// @interface BCXCallOptions : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	interface BCXCallOptions : INSCopying
	{
		// @property (readonly, copy, nonatomic) NSNumber * _Nullable recording;
		[NullAllowed, Export ("recording", ArgumentSemantic.Copy)]
		NSNumber Recording { get; }

		// @property (readonly, copy, nonatomic) NSNumber * _Nullable duration;
		[NullAllowed, Export ("duration", ArgumentSemantic.Copy)]
		NSNumber Duration { get; }

		// @property (readonly, assign, nonatomic) BCXCallType callType;
		[Export ("callType", ArgumentSemantic.Assign)]
		BCXCallType CallType { get; }

		// -(BOOL)isAudioVideo;
		[Export ("isAudioVideo")]
		bool IsAudioVideo { get; }

		// -(BOOL)isAudioUpgradable;
		[Export ("isAudioUpgradable")]
		bool IsAudioUpgradable { get; }

		// -(BOOL)isAudioOnly;
		[Export ("isAudioOnly")]
		bool IsAudioOnly { get; }

		// -(BCXCallOptions * _Nonnull)optionsWithRecording:(BOOL)recording;
		[Export ("optionsWithRecording:")]
		BCXCallOptions OptionsWithRecording (bool recording);

		// -(BCXCallOptions * _Nonnull)optionsWithDuration:(NSUInteger)duration;
		[Export ("optionsWithDuration:")]
		BCXCallOptions OptionsWithDuration (nuint duration);

		// -(BCXCallOptions * _Nonnull)optionsWithCallType:(BCXCallType)callType;
		[Export ("optionsWithCallType:")]
		BCXCallOptions OptionsWithCallType (BCXCallType callType);

		// +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording;
		[Static]
		[Export ("optionsWithRecording:")]
		BCXCallOptions MakeOptionsWithRecording (bool recording);

		// +(instancetype _Nonnull)optionsWithDuration:(NSUInteger)duration;
		[Static]
		[Export ("optionsWithDuration:")]
		BCXCallOptions MakeOptionsWithDuration (nuint duration);

		// +(instancetype _Nonnull)optionsWithCallType:(BCXCallType)callType;
		[Static]
		[Export ("optionsWithCallType:")]
		BCXCallOptions MakeOptionsWithCallType (BCXCallType callType);

		// +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording duration:(NSUInteger)duration;
		[Static]
		[Export ("optionsWithRecording:duration:")]
		BCXCallOptions MakeOptionsWithRecording (bool recording, nuint duration);

		// +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording duration:(NSUInteger)duration callType:(BCXCallType)callType;
		[Static]
		[Export ("optionsWithRecording:duration:callType:")]
		BCXCallOptions MakeOptionsWithRecording (bool recording, nuint duration, BCXCallType callType);

		// +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withRecording:(BOOL)recording;
		[Static]
		[Export ("optionsFromOptions:withRecording:")]
		BCXCallOptions MakeOptionsFromOptions (BCXCallOptions options, bool recording);

		// +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withDuration:(NSUInteger)duration;
		[Static]
		[Export ("optionsFromOptions:withDuration:")]
		BCXCallOptions MakeOptionsFromOptions (BCXCallOptions options, nuint duration);

		// +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withCallType:(BCXCallType)callType;
		[Static]
		[Export ("optionsFromOptions:withCallType:")]
		BCXCallOptions MakeOptionsFromOptions (BCXCallOptions options, BCXCallType callType);
	}

	// @protocol BCXCallObserver <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BCXCallObserver
	{
		// @optional -(void)call:(id<BCXCall> _Nonnull)call didChangeState:(BCXCallState)state;
		[Export ("call:didChangeState:")]
		void CallDidChangeState (IBCXCall call, BCXCallState state);

		// @optional -(void)call:(id<BCXCall> _Nonnull)call didUpdateOptions:(BCXCallOptions * _Nonnull)options;
		[Export ("call:didUpdateOptions:")]
		void CallDidUpdateOptions (IBCXCall call, BCXCallOptions options);

		// @optional -(void)call:(id<BCXCall> _Nonnull)call didUpdateParticipants:(id<BCXCallParticipants> _Nonnull)participants;
		[Export ("call:didUpdateParticipants:")]
		void CallDidUpdateParticipants (IBCXCall call, IBCXCallParticipants participants);

		// @optional -(void)callDidUpgradeToVideoCall:(id<BCXCall> _Nonnull)call;
		[Export ("callDidUpgradeToVideoCall:")]
		void CallDidUpgradeToVideoCall (IBCXCall call);

		// @optional -(void)callDidConnect:(id<BCXCall> _Nonnull)call;
		[Export ("callDidConnect:")]
		void CallDidConnect (IBCXCall call);

		// @optional -(void)callDidEnd:(id<BCXCall> _Nonnull)call;
		[Export ("callDidEnd:")]
		void CallDidEnd (IBCXCall call);

		// @optional -(void)call:(id<BCXCall> _Nonnull)call didFailWithError:(NSError * _Nonnull)error;
		[Export ("call:didFailWithError:")]
		void CallDidFailWithError (IBCXCall call, NSError error);
	}
	
	// @protocol BCXCall <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCXCall
	{
		// @required @property (readonly, nonatomic, strong) NSUUID * _Nonnull uuid;
		[Abstract]
		[Export ("uuid", ArgumentSemantic.Strong)]
		NSUuid Uuid { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nullable sid;
		[Abstract]
		[NullAllowed, Export ("sid", ArgumentSemantic.Strong)]
		string Sid { get; }

		// @required @property (readonly, nonatomic, strong) BCXCallOptions * _Nullable options;
		[Abstract]
		[NullAllowed, Export ("options", ArgumentSemantic.Strong)]
		BCXCallOptions Options { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallEndReason endReason;
		[Abstract]
		[Export ("endReason", ArgumentSemantic.Assign)]
		BCXCallEndReason EndReason { get; }

		// @required @property (readonly, assign, nonatomic) BCXDeclineReason declineReason;
		[Abstract]
		[Export ("declineReason", ArgumentSemantic.Assign)]
		BCXDeclineReason DeclineReason { get; }
		
		// @required -(void)addObserver:(id<BCXCallObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
		[Abstract]
		[Export ("addObserver:")]
		void AddObserver (IBCXCallObserver observer);

		// @required -(void)addObserver:(id<BCXCallObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
		[Abstract]
		[Export ("addObserver:queue:")]
		void AddObserver (IBCXCallObserver observer, [NullAllowed] DispatchQueue queue);

		// @required -(void)removeObserver:(id<BCXCallObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
		[Abstract]
		[Export ("removeObserver:")]
		void RemoveObserver (IBCXCallObserver observer);

		// @required @property (readonly, nonatomic, strong) id<BCXCallParticipants> _Nonnull participants;
		[Abstract]
		[Export ("participants", ArgumentSemantic.Strong)]
		IBCXCallParticipants Participants { get; }

		// @required -(BOOL)isGroupCall;
		[Abstract]
		[Export ("isGroupCall")]
		bool IsGroupCall { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallDirection direction;
		[Abstract]
		[Export ("direction", ArgumentSemantic.Assign)]
		BCXCallDirection Direction { get; }

		// @required -(BOOL)isIncoming;
		[Abstract]
		[Export ("isIncoming")]
		bool IsIncoming { get; }

		// @required -(BOOL)isOutgoing;
		[Abstract]
		[Export ("isOutgoing")]
		bool IsOutgoing { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallType callType;
		[Abstract]
		[Export ("callType", ArgumentSemantic.Assign)]
		BCXCallType CallType { get; }

		// @required -(BOOL)isAudioVideo;
		[Abstract]
		[Export ("isAudioVideo")]
		bool IsAudioVideo { get; }

		// @required -(BOOL)isAudioUpgradable;
		[Abstract]
		[Export ("isAudioUpgradable")]
		bool IsAudioUpgradable { get; }

		// @required -(BOOL)isAudioOnly;
		[Abstract]
		[Export ("isAudioOnly")]
		bool IsAudioOnly { get; }

		// @required -(BOOL)canUpgradeToVideo;
		[Abstract]
		[Export ("canUpgradeToVideo")]
		bool CanUpgradeToVideo { get; }

		// @required -(BOOL)didUpgradeToVideo;
		[Abstract]
		[Export ("didUpgradeToVideo")]
		bool DidUpgradeToVideo { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallState state;
		[Abstract]
		[Export ("state", ArgumentSemantic.Assign)]
		BCXCallState State { get; }

		// @required -(BOOL)hasEnded;
		[Abstract]
		[Export ("hasEnded")]
		bool HasEnded { get; }

		// @required -(BOOL)hasFailed;
		[Abstract]
		[Export ("hasFailed")]
		bool HasFailed { get; }

		// @required -(BOOL)isIdle;
		[Abstract]
		[Export ("isIdle")]
		bool IsIdle { get; }

		// @required -(BOOL)isRinging;
		[Abstract]
		[Export ("isRinging")]
		bool IsRinging { get; }

		// @required -(BOOL)isDialing;
		[Abstract]
		[Export ("isDialing")]
		bool IsDialing { get; }

		// @required -(BOOL)isConnecting;
		[Abstract]
		[Export ("isConnecting")]
		bool IsConnecting { get; }

		// @required -(BOOL)isConnected;
		[Abstract]
		[Export ("isConnected")]
		bool IsConnected { get; }

		// @required -(BOOL)isAnswering;
		[Abstract]
		[Export ("isAnswering")]
		bool IsAnswering { get; }

		// @required -(BOOL)isDeclining;
		[Abstract]
		[Export ("isDeclining")]
		bool IsDeclining { get; }

		// @required -(BOOL)isHangingUp;
		[Abstract]
		[Export ("isHangingUp")]
		bool IsHangingUp { get; }

		// @required @property (getter = isMuted, assign, readwrite, nonatomic) BOOL muted;
		[Abstract]
		[Export ("muted")]
		bool Muted { [Bind ("isMuted")] get; set; }
	}

	// @protocol BCXCallRegistry <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCXCallRegistry
	{
		// @required @property (readonly, copy, nonatomic) NSArray<id<BCXCall>> * _Nonnull calls;
		[Abstract]
		[Export ("calls", ArgumentSemantic.Copy)]
		IBCXCall[] Calls { get; }

		// @required @property (readonly, getter = isBusy, assign, nonatomic) BOOL busy;
		[Abstract]
		[Export ("busy")]
		bool Busy { [Bind ("isBusy")] get; }

		// @required -(id<BCXCall> _Nullable)callWithUUID:(NSUUID * _Nonnull)UUID;
		[Abstract]
		[Export ("callWithUUID:")]
		[return: NullAllowed]
		IBCXCall CallWithUUID (NSUuid UUID);

		// @required -(void)addObserver:(id<BCXCallRegistryObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
		[Abstract]
		[Export ("addObserver:")]
		void AddObserver (IBCXCallRegistryObserver observer);

		// @required -(void)addObserver:(id<BCXCallRegistryObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
		[Abstract]
		[Export ("addObserver:queue:")]
		void AddObserver (IBCXCallRegistryObserver observer, [NullAllowed] DispatchQueue queue);

		// @required -(void)removeObserver:(id<BCXCallRegistryObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
		[Abstract]
		[Export ("removeObserver:")]
		void RemoveObserver (IBCXCallRegistryObserver observer);
	}

	// @protocol BCXCallRegistryObserver <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BCXCallRegistryObserver
	{
		// @required -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didAddCall:(id<BCXCall> _Nonnull)call;
		[Abstract]
		[Export ("registry:didAddCall:")]
		void DidAddCall (IBCXCallRegistry registry, IBCXCall call);

		// @required -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didRemoveCall:(id<BCXCall> _Nonnull)call;
		[Abstract]
		[Export ("registry:didRemoveCall:")]
		void DidRemoveCall (IBCXCallRegistry registry, IBCXCall call);
	}

	// @protocol BCXCallParticipantsObserver <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BCXCallParticipantsObserver
	{
		// @optional -(void)onCallParticipantStateChanged:(id<BCXCallParticipant> _Nonnull)participant;
		[Export ("onCallParticipantStateChanged:")]
		void OnCallParticipantStateChanged (IBCXCallParticipant participant);

		// @optional -(void)onCallParticipantUpgradedToVideo:(id<BCXCallParticipant> _Nonnull)participant;
		[Export ("onCallParticipantUpgradedToVideo:")]
		void OnCallParticipantUpgradedToVideo (IBCXCallParticipant participant);
	}
	
	// @protocol BDFUser <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BDFUser
	{
		// @required @property (readonly, nonatomic, strong) NSString * _Nonnull alias;
		[Abstract]
		[Export ("alias", ArgumentSemantic.Strong)]
		string Alias { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nullable firstName;
		[Abstract]
		[NullAllowed, Export ("firstName", ArgumentSemantic.Strong)]
		string FirstName { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nullable lastName;
		[Abstract]
		[NullAllowed, Export ("lastName", ArgumentSemantic.Strong)]
		string LastName { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nullable email;
		[Abstract]
		[NullAllowed, Export ("email", ArgumentSemantic.Strong)]
		string Email { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nullable imageFilename;
		[Abstract]
		[NullAllowed, Export ("imageFilename", ArgumentSemantic.Strong)]
		string ImageFilename { get; }
	}

	// @protocol BCXUser <BDFUser>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCXUser : BDFUser
	{
		// @required @property (readonly, assign, nonatomic) BCXUserStatus status;
		[Abstract]
		[Export ("status", ArgumentSemantic.Assign)]
		BCXUserStatus Status { get; }

		// @required -(BOOL)isBusy;
		[Abstract]
		[Export ("isBusy")]
		bool IsBusy { get; }

		// @required -(BOOL)isOnline;
		[Abstract]
		[Export ("isOnline")]
		bool IsOnline { get; }

		// @required -(BOOL)isOffline;
		[Abstract]
		[Export ("isOffline")]
		bool IsOffline { get; }

		// @required -(BOOL)canUpgradeToVideo;
		[Abstract]
		[Export ("canUpgradeToVideo")]
		bool CanUpgradeToVideo { get; }
	}

	// @protocol BCXCallParticipant <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCXCallParticipant
	{
		// @required @property (readonly, nonatomic, strong) id<BCXUser> _Nonnull user;
		[Abstract]
		[Export ("user", ArgumentSemantic.Strong)]
		IBCXUser User { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nonnull userId;
		[Abstract]
		[Export ("userId", ArgumentSemantic.Strong)]
		string UserId { get; }

		// @required @property (readonly, assign, nonatomic) BCXCallParticipantState state;
		[Abstract]
		[Export ("state", ArgumentSemantic.Assign)]
		BCXCallParticipantState State { get; }

		// @required -(BOOL)hasAnswered;
		[Abstract]
		[Export ("hasAnswered")]
		bool HasAnswered { get; }

		// @required -(BOOL)hasDeclined;
		[Abstract]
		[Export ("hasDeclined")]
		bool HasDeclined { get; }

		// @required -(BOOL)hasDeclinedByDoNotDisturb;
		[Abstract]
		[Export ("hasDeclinedByDoNotDisturb")]
		bool HasDeclinedByDoNotDisturb { get; }

		// @required -(BOOL)didNotAnswer;
		[Abstract]
		[Export ("didNotAnswer")]
		bool DidNotAnswer { get; }

		// @required -(BOOL)hasTimedOut;
		[Abstract]
		[Export ("hasTimedOut")]
		bool HasTimedOut { get; }

		// @required -(BOOL)hasDisconnected;
		[Abstract]
		[Export ("hasDisconnected")]
		bool HasDisconnected { get; }

		// @required -(BOOL)didUpgradeToVideo;
		[Abstract]
		[Export ("didUpgradeToVideo")]
		bool DidUpgradeToVideo { get; }
	}

	// @protocol BCXCallParticipants <NSObject>
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface BCXCallParticipants
	{
		// @required @property (readonly, nonatomic, strong) id<BCXCallParticipant> _Nonnull caller;
		[Abstract]
		[Export("caller", ArgumentSemantic.Strong)]
		IBCXCallParticipant Caller { get; }

		// @required @property (readonly, nonatomic, strong) NSString * _Nonnull callerId;
		[Abstract]
		[Export("callerId", ArgumentSemantic.Strong)]
		string CallerId { get; }

		// @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull callees;
		[Abstract]
		[Export("callees", ArgumentSemantic.Strong)]
		IBCXCallParticipant[] Callees { get; }

		// @required @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull calleeIds;
		[Abstract]
		[Export("calleeIds", ArgumentSemantic.Strong)]
		string[] CalleeIds { get; }

		// @required -(id<BCXCallParticipant> _Nullable)calleeWithIdentifier:(NSString * _Nonnull)identifier;
		[Abstract]
		[Export("calleeWithIdentifier:")]
		[return: NullAllowed]
		IBCXCallParticipant CalleeWithIdentifier(string identifier);

		// @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull allParticipants;
		[Abstract]
		[Export("allParticipants", ArgumentSemantic.Strong)]
		IBCXCallParticipant[] AllParticipants { get; }

		// @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull opponents;
		[Abstract]
		[Export("opponents", ArgumentSemantic.Strong)]
		IBCXCallParticipant[] Opponents { get; }

		// @required @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull participantsIds;
		[Abstract]
		[Export("participantsIds", ArgumentSemantic.Strong)]
		string[] ParticipantsIds { get; }

		// @required -(id<BCXCallParticipant> _Nullable)participantWithIdentifier:(NSString * _Nonnull)identifier;
		[Abstract]
		[Export("participantWithIdentifier:")]
		[return: NullAllowed]
		IBCXCallParticipant ParticipantWithIdentifier(string identifier);

		// @required -(id<BCXCallParticipant> _Nullable)authenticatedUserParticipant;
		[Abstract]
		[NullAllowed, Export("authenticatedUserParticipant")]
		IBCXCallParticipant AuthenticatedUserParticipant { get; }

		// @required -(BOOL)isAuthenticatedUserTheCaller;
		[Abstract]
		[Export("isAuthenticatedUserTheCaller")]
		bool IsAuthenticatedUserTheCaller { get; }

		// @required -(void)addObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
		[Abstract]
		[Export("addObserver:")]
		void AddObserver(IBCXCallParticipantsObserver observer);

		// @required -(void)addObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
		[Abstract]
		[Export("addObserver:queue:")]
		void AddObserver(IBCXCallParticipantsObserver observer, [NullAllowed] DispatchQueue queue);

		// @required -(void)removeObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
		[Abstract]
		[Export("removeObserver:")]
		void RemoveObserver(IBCXCallParticipantsObserver observer);

		// @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)busy;
		[Abstract] 
		[Export("busy")] 
		IBCXCallParticipant[] Busy { get; }

		// @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)online;
		[Abstract] 
		[Export("online")] 
		IBCXCallParticipant[] Online { get; }

		// @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)offline;
		[Abstract] 
		[Export("offline")] 
		IBCXCallParticipant[] Offline { get; }

		// @required -(BOOL)hasEverybodyDeclined;
		[Abstract]
		[Export("hasEverybodyDeclined")]
		bool HasEverybodyDeclined { get; }

		// @required -(BOOL)hasAnybodyAnswered;
		[Abstract]
		[Export("hasAnybodyAnswered")]
		bool HasAnybodyAnswered { get; }

		// @required -(BOOL)hasAnybodyUpgradedToVideo;
		[Abstract]
		[Export("hasAnybodyUpgradedToVideo")]
		bool HasAnybodyUpgradedToVideo { get; }

		// @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)upgradedToVideo;
		[Abstract] 
		[Export("upgradedToVideo")] 
		IBCXCallParticipant[] UpgradedToVideo { get; }
	}

	[Static]
	partial interface Constants
	{
		// extern NSString *const _Nonnull kBCXErrorDomain;
		[Field ("kBCXErrorDomain", "__Internal")]
		NSString kBCXErrorDomain { get; }
	}

	// @interface BCXError : NSError
	[BaseType (typeof(NSError))]
	[DisableDefaultCtor]
	interface BCXError
	{
	}

	// @protocol BCHChatClientObserver <NSObject>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BCHChatClientObserver
	{
		// @optional -(void)chatClientWillStart:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientWillStart:")]
		void ChatClientWillStart (IBCHChatClient client);

		// @optional -(void)chatClientDidStart:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientDidStart:")]
		void ChatClientDidStart (IBCHChatClient client);

		// @optional -(void)chatClientWillPause:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientWillPause:")]
		void ChatClientWillPause (IBCHChatClient client);

		// @optional -(void)chatClientDidPause:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientDidPause:")]
		void ChatClientDidPause (IBCHChatClient client);

		// @optional -(void)chatClientWillStop:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientWillStop:")]
		void ChatClientWillStop (IBCHChatClient client);

		// @optional -(void)chatClientDidStop:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientDidStop:")]
		void ChatClientDidStop (IBCHChatClient client);

		// @optional -(void)chatClientWillResume:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientWillResume:")]
		void ChatClientWillResume (IBCHChatClient client);

		// @optional -(void)chatClientDidResume:(id<BCHChatClient> _Nonnull)client;
		[Export ("chatClientDidResume:")]
		void ChatClientDidResume (IBCHChatClient client);

		// @optional -(void)chatClient:(id<BCHChatClient> _Nonnull)client didFailWithError:(NSError * _Nonnull)error;
		[Export ("chatClient:didFailWithError:")]
        void ChatClientDidFailWithError(IBCHChatClient client, NSError error);
	}

	// @protocol BCHChatClient <NSObject>
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BCHChatClient
	{
		// @required @property (readonly, assign, nonatomic) BCHChatClientState state;
		[Abstract]
		[Export ("state", ArgumentSemantic.Assign)]
		BCHChatClientState State { get; }

		// @required -(void)addObserver:(id<BCHChatClientObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
		[Abstract]
		[Export ("addObserver:")]
		void AddObserver (IBCHChatClientObserver observer);

		// @required -(void)addObserver:(id<BCHChatClientObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
		[Abstract]
		[Export ("addObserver:queue:")]
		void AddObserver (IBCHChatClientObserver observer, [NullAllowed] DispatchQueue queue);

		// @required -(void)removeObserver:(id<BCHChatClientObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
		[Abstract]
		[Export ("removeObserver:")]
		void RemoveObserver (IBCHChatClientObserver observer);

		// @required -(void)start:(NSString * _Nonnull)userId __attribute__((swift_name("start(userId:)")));
		[Abstract]
		[Export ("start:")]
		void Start (string userId);

		// @required -(void)resume;
		[Abstract]
		[Export ("resume")]
		void Resume ();

		// @required -(void)pause;
		[Abstract]
		[Export ("pause")]
		void Pause ();

		// @required -(void)stop;
		[Abstract]
		[Export ("stop")]
		void Stop ();
	}

	// @protocol BCXHandleProvider <NSObject, NSCopying>
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	[Introduced (PlatformName.iOS, 10, 0)]
	interface BCXHandleProvider : INSCopying
	{
		// @required -(void)handleForAliases:(NSArray<NSString *> * _Nullable)aliases completion:(void (^ _Nonnull)(CXHandle * _Nonnull))completion __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Abstract]
		[Export ("handleForAliases:completion:")]
		void HandleForAliases ([NullAllowed] string[] aliases, Action<CXHandle> completion);
	}

	// @protocol BDKInAppChatNotificationTouchListener
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKInAppChatNotificationTouchListener
	{
		// @required -(void)didTouchChatNotification:(BDKChatNotification * _Nonnull)notification __attribute__((swift_name("onTouch(_:)")));
		[Abstract]
		[Export ("didTouchChatNotification:")]
		void DidTouchChatNotification (BDKChatNotification notification);
	}

	// @protocol BDKInAppFileShareNotificationTouchListener
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKInAppFileShareNotificationTouchListener
	{
		// @required -(void)didTouchFileShareNotification:(BDKFileShareNotification * _Nonnull)notification __attribute__((swift_name("onTouch(_:)")));
		[Abstract]
		[Export ("didTouchFileShareNotification:")]
		void DidTouchFileShareNotification (BDKFileShareNotification notification);
	}

	// @protocol BDKInAppNotificationsCoordinator
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface BDKInAppNotificationsCoordinator
	{
		// @required @property (nonatomic, strong) id<BDKInAppChatNotificationTouchListener> _Nullable chatListener;
		[Abstract]
		[NullAllowed, Export ("chatListener", ArgumentSemantic.Strong)]
		IBDKInAppChatNotificationTouchListener ChatListener { get; set; }

		// @required @property (nonatomic, strong) id<BDKInAppFileShareNotificationTouchListener> _Nullable fileShareListener;
		[Abstract]
		[NullAllowed, Export ("fileShareListener", ArgumentSemantic.Strong)]
		IBDKInAppFileShareNotificationTouchListener FileShareListener { get; set; }

		// @required @property (nonatomic, strong) NSFormatter * _Nullable formatter;
		[Abstract]
		[NullAllowed, Export ("formatter", ArgumentSemantic.Strong)]
		NSFormatter Formatter { get; set; }

		// @required -(void)start;
		[Abstract]
		[Export ("start")]
		void Start ();

		// @required -(void)stop;
		[Abstract]
		[Export ("stop")]
		void Stop ();
	}

	// @interface BDKChatNotification : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKChatNotification
	{
	}

	// @interface BDKFileShareNotification : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKFileShareNotification
	{
	}

	// @interface BCXAdditions (PKPushCredentials)
	[Category]
	[BaseType (typeof(PKPushCredentials))]
	interface PKPushCredentials_BCXAdditions
	{
		// @property (readonly, copy, nonatomic) NS_SWIFT_NAME(tokenAsString) NSString * bcx_tokenAsString __attribute__((swift_name("tokenAsString")));
		[Export("bcx_tokenAsString")]
		string Bcx_tokenAsString();
	}

	// @interface BDKEnvironment : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BDKEnvironment : INSCopying
	{
		// @property (readonly, nonatomic, class) BDKEnvironment * _Nonnull production;
		[Static]
		[Export ("production")]
		BDKEnvironment Production { get; }

		// @property (readonly, nonatomic, class) BDKEnvironment * _Nonnull sandbox;
		[Static]
		[Export ("sandbox")]
		BDKEnvironment Sandbox { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nonnull name;
		[Export ("name", ArgumentSemantic.Strong)]
		string Name { get; }
	}
	
	// @interface BDKConfig : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	interface BDKConfig : INSCopying
	{
		// @property (copy, nonatomic, null_resettable) id<BDKUserInfoFetcher> _Null_unspecified userInfoFetcher;
		[NullAllowed, Export ("userInfoFetcher", ArgumentSemantic.Copy)]
		IBDKUserInfoFetcher UserInfoFetcher { get; set; }

		// @property (copy, nonatomic) BDKEnvironment * _Nonnull environment;
		[Export ("environment", ArgumentSemantic.Copy)]
		BDKEnvironment Environment { get; set; }

		// @property (getter = isCallKitEnabled, assign, nonatomic) BOOL callKitEnabled;
		[Export ("callKitEnabled")]
		bool CallKitEnabled { [Bind ("isCallKitEnabled")] get; set; }

		// @property (copy, nonatomic) API_AVAILABLE(ios(10.0)) NSSet<NSNumber *> * supportedHandleTypes __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Export ("supportedHandleTypes", ArgumentSemantic.Copy)]
		NSSet<NSNumber> SupportedHandleTypes { get; set; }

		// @property (copy, nonatomic) API_AVAILABLE(ios(10.0)) NSString * nativeUILocalizedName __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Export ("nativeUILocalizedName")]
		string NativeUILocalizedName { get; set; }

		// @property (copy, nonatomic) API_AVAILABLE(ios(10.0)) NSString * nativeUIRingToneSound __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Export ("nativeUIRingToneSound")]
		string NativeUIRingToneSound { get; set; }

		// @property (copy, nonatomic) API_AVAILABLE(ios(10.0)) NSData * nativeUITemplateIconImageData __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Export ("nativeUITemplateIconImageData", ArgumentSemantic.Copy)]
		NSData NativeUITemplateIconImageData { get; set; }

		// @property (nonatomic, strong, null_resettable) API_AVAILABLE(ios(10.0)) id<BCXHandleProvider> handleProvider __attribute__((availability(ios, introduced=10.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[NullAllowed, Export ("handleProvider", ArgumentSemantic.Strong)]
		IBCXHandleProvider HandleProvider { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull notificationPayloadKeyPath;
		[Export ("notificationPayloadKeyPath")]
		string NotificationPayloadKeyPath { get; set; }

		[Wrap ("WeakPushRegistryDelegate")]
		IPKPushRegistryDelegate PushRegistryDelegate { get; set; }

		// @property (nonatomic, strong) id<PKPushRegistryDelegate> _Nonnull pushRegistryDelegate;
		[NullAllowed, Export ("pushRegistryDelegate", ArgumentSemantic.Strong)]
		NSObject WeakPushRegistryDelegate { get; set; }

		// @property (assign, nonatomic) BDKSpeakerHijackingStrategy speakerHijackingStrategy;
		[Export ("speakerHijackingStrategy", ArgumentSemantic.Assign)]
		BDKSpeakerHijackingStrategy SpeakerHijackingStrategy { get; set; }

		// @property (nonatomic, class) BDFDDLogLevel logLevel;
		[Static]
		[Export ("logLevel", ArgumentSemantic.Assign)]
		BDFDDLogLevel LogLevel { get; set; }

		// @property (readonly, nonatomic, class) NSInteger logContext;
		[Static]
		[Export ("logContext")]
		nint LogContext { get; }

		// @property (readonly, nonatomic, class) NSString * _Nonnull logTag;
		[Static]
		[Export ("logTag")]
		string LogTag { get; }
		
		// +(instancetype _Nonnull)new;
		[Static]
		[Export ("new")]
		BDKConfig New ();
	}
	
	// @interface BDKCallBannerController : NSObject
	[BaseType (typeof(NSObject))]
	interface BDKCallBannerController
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		IBDKCallBannerControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<BDKCallBannerControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) UIViewController * _Nullable parentViewController;
		[NullAllowed, Export ("parentViewController", ArgumentSemantic.Strong)]
		UIViewController ParentViewController { get; set; }

		// -(void)show;
		[Export ("show")]
		void Show ();

		// -(void)hide;
		[Export ("hide")]
		void Hide ();

		// -(void)viewWillTransitionTo:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
		[Export ("viewWillTransitionTo:withTransitionCoordinator:")]
		void ViewWillTransitionTo (CGSize size, IUIViewControllerTransitionCoordinator coordinator);
	}

	// @protocol BDKCallBannerControllerDelegate
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKCallBannerControllerDelegate
	{
		// @required -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller didTouch:(BDKCallBannerView * _Nonnull)banner;
		[Abstract]
		[Export ("callBannerController:didTouch:")]
		void DidTouch (BDKCallBannerController controller, BDKCallBannerView banner);

		// @optional -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willShow:(BDKCallBannerView * _Nonnull)banner;
		[Export ("callBannerController:willShow:")]
		void WillShow (BDKCallBannerController controller, BDKCallBannerView banner);

		// @optional -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willHide:(BDKCallBannerView * _Nonnull)banner;
		[Export ("callBannerController:willHide:")]
		void WillHide (BDKCallBannerController controller, BDKCallBannerView banner);
	}

	// @interface BDKCallBannerView : UIView
	[BaseType (typeof(UIView))]
	interface BDKCallBannerView
	{
	}

	// @interface BDKCallPresentationErrorDomain : NSObject
	[BaseType (typeof(NSObject))]
	interface BDKCallPresentationErrorDomain
	{
		// @property (readonly, copy, nonatomic, class) NSString * _Nonnull value;
		[Static]
		[Export ("value")]
		string Value { get; }
	}

	// @interface BDKCallWindow : UIWindow
	[BaseType (typeof(UIWindow))]
	interface BDKCallWindow
	{
		// @property (readonly, nonatomic, weak, class) BDKCallWindow * _Nullable instance;
		[Static]
		[NullAllowed, Export ("instance", ArgumentSemantic.Weak)]
		BDKCallWindow Instance { get; }

		[Wrap ("WeakCallDelegate")]
		[NullAllowed]
		IBDKCallWindowDelegate CallDelegate { get; set; }

		// @property (nonatomic, weak) id<BDKCallWindowDelegate> _Nullable callDelegate;
		[NullAllowed, Export ("callDelegate", ArgumentSemantic.Weak)]
		NSObject WeakCallDelegate { get; set; }

		// @property (readonly, nonatomic, strong) id<BDKIntent> _Nullable intent;
		[NullAllowed, Export ("intent", ArgumentSemantic.Strong)]
		IBDKIntent Intent { get; }

		// -(instancetype _Nonnull)initWithWindowScene:(UIWindowScene * _Nonnull)windowScene __attribute__((objc_designated_initializer)) __attribute__((availability(ios, introduced=13.0)));
		[Introduced (PlatformName.iOS, 13, 0)]
		[Export ("initWithWindowScene:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIWindowScene windowScene);

		// -(void)shouldPresentCallViewControllerWithIntent:(id<BDKIntent> _Nullable)intent completion:(void (^ _Nonnull)(BOOL))completion __attribute__((deprecated("Use presentCallViewController(for:completion:) method instead.")));
		[Export ("shouldPresentCallViewControllerWithIntent:completion:")]
		[Obsolete("Method deprecated, use PresentCallViewControllerWithCompletion instead")]
		void ShouldPresentCallViewControllerWithIntent ([NullAllowed] IBDKIntent intent, Action<bool> completion);

		// -(void)presentCallViewControllerFor:(id<BDKIntent> _Nonnull)intent completion:(void (^ _Nullable)(NSError * _Nullable))completion;
		[Export ("presentCallViewControllerFor:completion:")]
		void PresentCallViewControllerWithCompletion (IBDKIntent intent, [NullAllowed] Action<NSError> completion);

		// -(void)dismissCallViewControllerWithCompletion:(void (^ _Nonnull)(void))completion;
		[Export ("dismissCallViewControllerWithCompletion:")]
		void DismissCallViewControllerWithCompletion (Action completion);

		// -(void)setConfiguration:(BDKCallViewControllerConfiguration * _Nullable)configuration;
		[Export ("setConfiguration:")]
		void SetConfiguration ([NullAllowed] BDKCallViewControllerConfiguration configuration);

		// -(void)handleINStartVideoCallIntent:(INStartVideoCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=10.0, deprecated=13.0)));
		[Introduced (PlatformName.iOS, 10, 0)]
		[Deprecated (PlatformName.iOS, 13, 0, message: "handle(startVideoCallIntent:) is deprecated. Please use handle(startCallIntent:) instead")]
		[Export ("handleINStartVideoCallIntent:")]
		void HandleINStartVideoCallIntent (INStartVideoCallIntent intent);

		// -(void)handleINStartCallIntent:(INStartCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=13.0)));
		[Introduced (PlatformName.iOS, 13, 0)]
		[Export ("handleINStartCallIntent:")]
		void HandleINStartCallIntent (INStartCallIntent intent);
	}
	
	// @protocol BDKCallWindowDelegate
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	interface BDKCallWindowDelegate
	{
		// @required -(void)callWindowDidFinish:(BDKCallWindow * _Nonnull)window;
		[Abstract]
		[Export ("callWindowDidFinish:")]
		void CallWindowDidFinish (BDKCallWindow window);

		// @optional -(void)callWindow:(BDKCallWindow * _Nonnull)window openChatWith:(BCHOpenChatIntent * _Nonnull)intent;
		[Export ("callWindow:openChatWith:")]
		void CallWindowOpenChatWith (BDKCallWindow window, BCHOpenChatIntent intent);
	}

	// @interface BCHChannelViewController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface BCHChannelViewController
	{
		// @property (nonatomic, strong) BCHChannelViewControllerConfiguration * _Nullable configuration;
		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		BCHChannelViewControllerConfiguration Configuration { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		IBCHChannelViewControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<BCHChannelViewControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) BCHOpenChatIntent * _Nullable intent;
		[NullAllowed, Export ("intent", ArgumentSemantic.Strong)]
		BCHOpenChatIntent Intent { get; set; }

		// -(instancetype _Nonnull)initWithNibName:(NSString * _Nullable)nibNameOrNil bundle:(NSBundle * _Nullable)nibBundleOrNil __attribute__((objc_designated_initializer));
		[Export ("initWithNibName:bundle:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string nibNameOrNil, [NullAllowed] NSBundle nibBundleOrNil);
	}
	
	// @interface BCHChannelViewControllerConfiguration : NSObject
	[BaseType (typeof(NSObject))]
	interface BCHChannelViewControllerConfiguration
	{
		// -(instancetype _Nonnull)initWithUserInfoFetcher:(id<BDKUserInfoFetcher> _Nonnull)userInfoFetcher;
		[Export ("initWithUserInfoFetcher:")]
		IntPtr Constructor (IBDKUserInfoFetcher userInfoFetcher);

		// -(instancetype _Nonnull)initWithFormatter:(NSFormatter * _Nonnull)formatter;
		[Export ("initWithFormatter:")]
		IntPtr Constructor (NSFormatter formatter);

		// -(instancetype _Nonnull)initWithAudioButton:(BOOL)audioButton videoButton:(BOOL)videoButton userInfoFetcher:(id<BDKUserInfoFetcher> _Nullable)userInfoFetcher formatter:(NSFormatter * _Nullable)formatter __attribute__((objc_designated_initializer));
		[Export ("initWithAudioButton:videoButton:userInfoFetcher:formatter:")]
		[DesignatedInitializer]
		IntPtr Constructor (bool audioButton, bool videoButton, [NullAllowed] IBDKUserInfoFetcher userInfoFetcher, [NullAllowed] NSFormatter formatter);

		// -(instancetype _Nonnull)initWithAudioButton:(BOOL)audioButton videoButton:(BOOL)videoButton userInfoFetcher:(id<BDKUserInfoFetcher> _Nullable)userInfoFetcher;
		[Export ("initWithAudioButton:videoButton:userInfoFetcher:")]
		IntPtr Constructor (bool audioButton, bool videoButton, [NullAllowed] IBDKUserInfoFetcher userInfoFetcher);

		// -(instancetype _Nonnull)initWithAudioButton:(BOOL)audioButton videoButton:(BOOL)videoButton formatter:(NSFormatter * _Nullable)formatter;
		[Export ("initWithAudioButton:videoButton:formatter:")]
		IntPtr Constructor (bool audioButton, bool videoButton, [NullAllowed] NSFormatter formatter);
	}

	// @protocol BCHChannelViewControllerDelegate
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface BCHChannelViewControllerDelegate
	{
		// @required -(void)channelViewControllerDidFinish:(BCHChannelViewController * _Nonnull)controller;
		[Abstract]
		[Export ("channelViewControllerDidFinish:")]
		void ChannelViewControllerDidFinish (BCHChannelViewController controller);

		// @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTouchNotification:(BDKChatNotification * _Nonnull)notification __attribute__((deprecated("This method is deprecated and will be removed soon. In-app notifications are now handled by the InAppNotificationsCoordinator. Register as a InAppChatNotificationTouchListener on the InAppNotificationsCoordinator instance provided by the SDK singleton instead")));
		[Export ("channelViewController:didTouchNotification:")]
		[Obsolete("This method is deprecated and will be removed soon. In-app notifications are now handled by the InAppNotificationsCoordinator. Register as a InAppChatNotificationTouchListener on the InAppNotificationsCoordinator instance provided by the SDK singleton instead")]
		void ChannelViewControllerDidTouchNotification (BCHChannelViewController controller, BDKChatNotification notification);

		// @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTouchBanner:(BDKCallBannerView * _Nonnull)banner;
		[Abstract]
		[Export ("channelViewController:didTouchBanner:")]
		void ChannelViewControllerDidTouchBanner (BCHChannelViewController controller, BDKCallBannerView banner);

		// @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller willHide:(BDKCallBannerView * _Nonnull)banner;
		[Export ("channelViewController:willHide:")]
		void ChannelViewControllerWillHide (BCHChannelViewController controller, BDKCallBannerView banner);

		// @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller willShow:(BDKCallBannerView * _Nonnull)banner;
		[Export ("channelViewController:willShow:")]
		void ChannelViewControllerWillShow (BCHChannelViewController controller, BDKCallBannerView banner);

		// @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTapAudioCallWith:(NSArray<NSString *> * _Nonnull)users;
		[Abstract]
		[Export ("channelViewController:didTapAudioCallWith:")]
		void ChannelViewControllerDidTapAudioCallWith (BCHChannelViewController controller, string[] users);

		// @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTapVideoCallWith:(NSArray<NSString *> * _Nonnull)users;
		[Abstract]
		[Export ("channelViewController:didTapVideoCallWith:")]
		void ChannelViewControllerDidTapVideoCallWith (BCHChannelViewController controller, string[] users);
	}

	// @interface BCHMessageNotificationController : NSObject
	[BaseType (typeof(NSObject))]
	[Obsolete("BCHMessageNotificationController is deprecated, use INAppNotificationsCoordinator instead")]
	interface BCHMessageNotificationController
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		IBCHMessageNotificationControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<BCHMessageNotificationControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, weak) UIViewController * _Nullable parentViewController;
		[NullAllowed, Export ("parentViewController", ArgumentSemantic.Weak)]
		UIViewController ParentViewController { get; set; }

		// @property (nonatomic, strong) BCHMessageNotificationControllerConfiguration * _Nullable configuration;
		[NullAllowed, Export ("configuration", ArgumentSemantic.Strong)]
		BCHMessageNotificationControllerConfiguration Configuration { get; set; }

		// -(void)show;
		[Export ("show")]
		void Show ();

		// -(void)hide;
		[Export ("hide")]
		void Hide ();

		// -(void)viewWillTransitionTo:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
		[Export ("viewWillTransitionTo:withTransitionCoordinator:")]
		void ViewWillTransitionTo (CGSize size, IUIViewControllerTransitionCoordinator coordinator);
	}
	
	// @interface BCHMessageNotificationControllerConfiguration : NSObject
	[BaseType (typeof(NSObject))]
	[Obsolete("BCHMessageNotificationControllerConfiguration is deprecated, take a look at INAppNotificationsCoordinator instead")]
	interface BCHMessageNotificationControllerConfiguration
	{
		// -(instancetype _Nonnull)initWithUserInfoFetcher:(id<BDKUserInfoFetcher> _Nullable)userInfoFetcher __attribute__((objc_designated_initializer));
		[Export ("initWithUserInfoFetcher:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] IBDKUserInfoFetcher userInfoFetcher);
	}

	// @protocol BCHMessageNotificationControllerDelegate
	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof(NSObject))]
	[Obsolete("BCHMessageNotificationControllerDelegate is deprecated, take a look at INAppNotificationsCoordinator instead")]
	interface BCHMessageNotificationControllerDelegate
	{
		// @required -(void)messageNotificationController:(BCHMessageNotificationController * _Nonnull)controller didTouch:(BDKChatNotification * _Nonnull)notification;
		[Abstract]
		[Export ("messageNotificationController:didTouch:")]
		void DidTouch (BCHMessageNotificationController controller, BDKChatNotification notification);
	}

	// @interface BCHOpenChatIntent : NSObject <BDKIntent>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface BCHOpenChatIntent : BDKIntent
	{
		// +(BCHOpenChatIntent * _Nonnull)openChatWith:(NSString * _Nonnull)participant __attribute__((warn_unused_result("")));
		[Static]
		[Export ("openChatWith:")]
		BCHOpenChatIntent OpenChatWith (string participant);

		// +(BCHOpenChatIntent * _Nullable)openChatFrom:(BDKChatNotification * _Nonnull)notification __attribute__((warn_unused_result("")));
		[Static]
		[Export ("openChatFrom:")]
		[return: NullAllowed]
		BCHOpenChatIntent OpenChatFrom (BDKChatNotification notification);
	}

	// @interface BDKOpenDownloadsIntent : NSObject <BDKIntent>
	[BaseType (typeof(NSObject))]
	interface BDKOpenDownloadsIntent : BDKIntent
	{
	}
}
