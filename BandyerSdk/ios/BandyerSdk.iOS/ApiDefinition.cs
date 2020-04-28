using System;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using Intents;
using ObjCRuntime;
using PushKit;
using UIKit;
using Bandyer;

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

        // @property (readonly, nonatomic, strong) id<BCXCallClient> _Nonnull callClient;
        [Export("callClient", ArgumentSemantic.Strong)]
        IBCXCallClient CallClient { get; }

        // @property (readonly, nonatomic, strong) id<BCHChatClient> _Nonnull chatClient;
        [Export("chatClient", ArgumentSemantic.Strong)]
        IBCHChatClient ChatClient { get; }

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

    // @protocol BDKIntent <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface BDKIntent
    {
        // @required @property (readonly, copy, nonatomic) NSUUID * _Nonnull UUID;
        [Abstract]
        [Export("UUID", ArgumentSemantic.Copy)]
        NSUuid UUID { get; }
    }

    interface IBDKIntent { }

    // @interface BDKMakeCallIntent : NSObject <BDKIntent>
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface BDKMakeCallIntent : BDKIntent
    {
        // @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull callee;
        [Export("callee", ArgumentSemantic.Copy)]
        string[] Callee { get; }

        // @property (readonly, getter = shouldRecord, assign, nonatomic) BOOL record;
        [Export("record")]
        bool Record { [Bind("shouldRecord")] get; }

        // @property (readonly, assign, nonatomic) NSUInteger maximumDuration;
        [Export("maximumDuration")]
        nuint MaximumDuration { get; }

        // @property (readonly, assign, nonatomic) BDKCallType callType;
        [Export("callType", ArgumentSemantic.Assign)]
        BDKCallType CallType { get; }

        // +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee;
        [Static]
        [Export("intentWithCallee:")]
        BDKMakeCallIntent IntentWithCallee(string[] callee);

        // +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type;
        [Static]
        [Export("intentWithCallee:type:")]
        BDKMakeCallIntent IntentWithCallee(string[] callee, BDKCallType type);

        // +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type record:(BOOL)record;
        [Static]
        [Export("intentWithCallee:type:record:")]
        BDKMakeCallIntent IntentWithCallee(string[] callee, BDKCallType type, bool record);

        // +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type maximumDuration:(NSUInteger)duration;
        [Static]
        [Export("intentWithCallee:type:maximumDuration:")]
        BDKMakeCallIntent IntentWithCallee(string[] callee, BDKCallType type, nuint duration);

        // +(instancetype _Nonnull)intentWithCallee:(NSArray<NSString *> * _Nonnull)callee type:(BDKCallType)type record:(BOOL)record maximumDuration:(NSUInteger)duration;
        [Static]
        [Export("intentWithCallee:type:record:maximumDuration:")]
        BDKMakeCallIntent IntentWithCallee(string[] callee, BDKCallType type, bool record, nuint duration);
    }

    //// @interface BDKJoinURLIntent : NSObject <BDKIntent>
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BDKJoinURLIntent : IBDKIntent
    //{
    //  // @property (readonly, copy, nonatomic) NSURL * _Nonnull url;
    //  [Export("url", ArgumentSemantic.Copy)]
    //  NSUrl Url { get; }

    //  // +(instancetype _Nonnull)intentWithURL:(NSURL * _Nonnull)url;
    //  [Static]
    //  [Export("intentWithURL:")]
    //  BDKJoinURLIntent IntentWithURL(NSUrl url);
    //}

    //// @interface BDKIncomingCallHandlingIntent : NSObject <BDKIntent>
    //[BaseType(typeof(NSObject))]
    //interface BDKIncomingCallHandlingIntent : IBDKIntent
    //{
    //}

    //// @protocol BDKCallViewControllerDelegate <NSObject>
    //[Protocol, Model(AutoGeneratedName = true)]
    //[BaseType(typeof(NSObject))]
    //interface BDKCallViewControllerDelegate
    //{
    //  // @required -(void)callViewControllerDidFinish:(BDKCallViewController * _Nonnull)controller;
    //  [Abstract]
    //  [Export("callViewControllerDidFinish:")]
    //  void CallViewControllerDidFinish(BDKCallViewController controller);

    //  // @required -(void)callViewControllerDidPressBack:(BDKCallViewController * _Nonnull)controller;
    //  [Abstract]
    //  [Export("callViewControllerDidPressBack:")]
    //  void CallViewControllerDidPressBack(BDKCallViewController controller);

    //  // @required -(void)callViewController:(BDKCallViewController * _Nonnull)controller openChatWith:(NSString * _Nonnull)participantId;
    //  [Abstract]
    //  [Export("callViewController:openChatWith:")]
    //  void CallViewController(BDKCallViewController controller, string participantId);
    //}

    //// @interface BDKCallViewController : UIViewController
    //[BaseType(typeof(UIViewController))]
    //interface BDKCallViewController
    //{
    //  [Wrap("WeakDelegate")]
    //  [NullAllowed]
    //  BDKCallViewControllerDelegate Delegate { get; set; }

    //  // @property (nonatomic, weak) id<BDKCallViewControllerDelegate> _Nullable delegate __attribute__((iboutlet));
    //  [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
    //  NSObject WeakDelegate { get; set; }

    //  // -(void)setConfiguration:(BDKCallViewControllerConfiguration * _Nonnull)configuration;
    //  [Export("setConfiguration:")]
    //  void SetConfiguration(BDKCallViewControllerConfiguration configuration);

    //  // -(void)handleIntent:(id<BDKIntent> _Nonnull)intent __attribute__((swift_name("handle(intent:)")));
    //  [Export("handleIntent:")]
    //  void HandleIntent(BDKIntent intent);

    //  // -(void)handleINStartVideoCallIntent:(INStartVideoCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=10.0, deprecated=13.0))) __attribute__((swift_name("handle(startVideoCallIntent:)")));
    //  [Introduced(PlatformName.iOS, 10, 0, message: "handleINStartVideoCallIntent: is deprecated. Please use handleINStartCallIntent: instead")]
    //  [Deprecated(PlatformName.iOS, 13, 0, message: "handleINStartVideoCallIntent: is deprecated. Please use handleINStartCallIntent: instead")]
    //  [Export("handleINStartVideoCallIntent:")]
    //  void HandleINStartVideoCallIntent(INStartVideoCallIntent intent);

    //  // -(void)handleINStartCallIntent:(INStartCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=13.0))) __attribute__((swift_name("handle(startCallIntent:)")));
    //  [iOS(13, 0)]
    //  [Export("handleINStartCallIntent:")]
    //  void HandleINStartCallIntent(INStartCallIntent intent);
    //}

    // @interface BDKCallViewControllerConfiguration : NSObject <NSCopying>
    [BaseType(typeof(NSObject))]
    interface BDKCallViewControllerConfiguration : INSCopying
    {
        //// @property (copy, nonatomic) id<BDKUserInfoFetcher> _Null_unspecified userInfoFetcher;
        //[Export("userInfoFetcher", ArgumentSemantic.Copy)]
        //BDKUserInfoFetcher UserInfoFetcher { get; set; }

        // @property (copy, nonatomic) NSFormatter * _Null_unspecified callInfoTitleFormatter;
        [Export("callInfoTitleFormatter", ArgumentSemantic.Copy)]
        NSFormatter CallInfoTitleFormatter { get; set; }

        // @property (copy, nonatomic) NSURL * _Nullable fakeCapturerFileURL;
        [NullAllowed, Export("fakeCapturerFileURL", ArgumentSemantic.Copy)]
        NSUrl FakeCapturerFileURL { get; set; }
    }

    //// @protocol BDKUserInfoFetcher <NSObject, NSCopying>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BDKUserInfoFetcher : INSCopying
    //{
    //  // @required -(void)fetchUsers:(NSArray<NSString *> * _Nonnull)aliases completion:(void (^ _Nonnull)(NSArray<BDKUserInfoDisplayItem *> * _Nullable))completion;
    //  [Abstract]
    //  [Export("fetchUsers:completion:")]
    //  void Completion(string[] aliases, Action<NSArray<BDKUserInfoDisplayItem>> completion);
    //}

    //// @interface BDKUserInfoDisplayItem : NSObject <NSCopying>
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BDKUserInfoDisplayItem : INSCopying
    //{
    //  // @property (readonly, copy, nonatomic) NSString * _Nonnull alias;
    //  [Export("alias")]
    //  string Alias { get; }

    //  // @property (copy, nonatomic) NSString * _Nullable firstName;
    //  [NullAllowed, Export("firstName")]
    //  string FirstName { get; set; }

    //  // @property (copy, nonatomic) NSString * _Nullable lastName;
    //  [NullAllowed, Export("lastName")]
    //  string LastName { get; set; }

    //  // @property (copy, nonatomic) NSString * _Nullable email;
    //  [NullAllowed, Export("email")]
    //  string Email { get; set; }

    //  // @property (copy, nonatomic) NSString * _Nullable nickname;
    //  [NullAllowed, Export("nickname")]
    //  string Nickname { get; set; }

    //  // @property (copy, nonatomic) NSURL * _Nullable imageURL;
    //  [NullAllowed, Export("imageURL", ArgumentSemantic.Copy)]
    //  NSUrl ImageURL { get; set; }

    //  // @property (readonly, copy, nonatomic) UIImage * _Nullable image;
    //  [NullAllowed, Export("image", ArgumentSemantic.Copy)]
    //  UIImage Image { get; }

    //  // -(instancetype _Nonnull)initWithAlias:(NSString * _Nonnull)alias;
    //  [Export("initWithAlias:")]
    //  IntPtr Constructor(string alias);
    //}

    // @protocol BCXCallClientObserver <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface BCXCallClientObserver
    {
        //// @optional -(void)callClient:(id<BCXCallClient> _Nonnull)client didReceiveIncomingCall:(id<BCXCall> _Nonnull)call;
        //[Export("callClient:didReceiveIncomingCall:")]
        //void CallClient(BCXCallClient client, BCXCall call);

        // @optional -(void)callClientWillStart:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientWillStart:")]
        void CallClientWillStart(BCXCallClient client);

        // @optional -(void)callClientDidStart:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientDidStart:")]
        void CallClientDidStart(BCXCallClient client);

        // @optional -(void)callClientDidStartReconnecting:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientDidStartReconnecting:")]
        void CallClientDidStartReconnecting(BCXCallClient client);

        // @optional -(void)callClientWillPause:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientWillPause:")]
        void CallClientWillPause(BCXCallClient client);

        // @optional -(void)callClientDidPause:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientDidPause:")]
        void CallClientDidPause(BCXCallClient client);

        // @optional -(void)callClientWillStop:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientWillStop:")]
        void CallClientWillStop(BCXCallClient client);

        // @optional -(void)callClientDidStop:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientDidStop:")]
        void CallClientDidStop(BCXCallClient client);

        // @optional -(void)callClientWillResume:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientWillResume:")]
        void CallClientWillResume(BCXCallClient client);

        // @optional -(void)callClientDidResume:(id<BCXCallClient> _Nonnull)client;
        [Export("callClientDidResume:")]
        void CallClientDidResume(BCXCallClient client);

        // @optional -(void)callClient:(id<BCXCallClient> _Nonnull)client didFailWithError:(NSError * _Nonnull)error;
        [Export("callClient:didFailWithError:")]
        void CallClientDidFailWithError(BCXCallClient client, NSError error);
    }

    interface IBCXCallClientObserver { }

    // @protocol BCXCallClient <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface BCXCallClient
    {
        //// @required @property (readonly, nonatomic, strong) id<BCXUser> _Nullable user;
        //[Abstract]
        //[NullAllowed, Export("user", ArgumentSemantic.Strong)]
        //BCXUser User { get; }

        //// @required @property (readonly, assign, nonatomic) BCXCallClientState state;
        //[Abstract]
        //[Export("state", ArgumentSemantic.Assign)]
        //BCXCallClientState State { get; }

        //// @required -(BOOL)isStopped;
        //[Abstract]
        //[Export("isStopped")]
        //[Verify(MethodToProperty)]
        //bool IsStopped { get; }

        //// @required -(BOOL)isStarting;
        //[Abstract]
        //[Export("isStarting")]
        //[Verify(MethodToProperty)]
        //bool IsStarting { get; }

        //// @required -(BOOL)isRunning;
        //[Abstract]
        //[Export("isRunning")]
        //[Verify(MethodToProperty)]
        //bool IsRunning { get; }

        //// @required -(BOOL)isPaused;
        //[Abstract]
        //[Export("isPaused")]
        //[Verify(MethodToProperty)]
        //bool IsPaused { get; }

        //// @required -(BOOL)isResuming;
        //[Abstract]
        //[Export("isResuming")]
        //[Verify(MethodToProperty)]
        //bool IsResuming { get; }

        //// @required -(BOOL)isReconnecting;
        //[Abstract]
        //[Export("isReconnecting")]
        //[Verify(MethodToProperty)]
        //bool IsReconnecting { get; }

        // @required -(void)addObserver:(id<BCXCallClientObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
        [Abstract]
        [Export("addObserver:")]
        void AddObserver(IBCXCallClientObserver observer);

        // @required -(void)addObserver:(id<BCXCallClientObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
        [Abstract]
        [Export("addObserver:queue:")]
        void AddObserver(IBCXCallClientObserver observer, [NullAllowed] DispatchQueue queue);

        // @required -(void)removeObserver:(id<BCXCallClientObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
        [Abstract]
        [Export("removeObserver:")]
        void RemoveObserver(IBCXCallClientObserver observer);

        // @required -(void)start:(NSString * _Nonnull)userId;
        [Abstract]
        [Export("start:")]
        void Start(string userId);

        // @required -(void)resume;
        [Abstract]
        [Export("resume")]
        void Resume();

        // @required -(void)pause;
        [Abstract]
        [Export("pause")]
        void Pause();

        // @required -(void)stop;
        [Abstract]
        [Export("stop")]
        void Stop();

        //// @required -(void)performAction:(id<BCXAction> _Nonnull)action;
        //[Abstract]
        //[Export("performAction:")]
        //void PerformAction(BCXAction action);
    }

    interface IBCXCallClient { }

    //// @interface BCXCallOptions : NSObject <NSCopying>
    //[BaseType(typeof(NSObject))]
    //interface BCXCallOptions : INSCopying
    //{
    //  // @property (readonly, copy, nonatomic) NSNumber * _Nullable recording;
    //  [NullAllowed, Export("recording", ArgumentSemantic.Copy)]
    //  NSNumber Recording { get; }

    //  // @property (readonly, copy, nonatomic) NSNumber * _Nullable duration;
    //  [NullAllowed, Export("duration", ArgumentSemantic.Copy)]
    //  NSNumber Duration { get; }

    //  // @property (readonly, assign, nonatomic) BCXCallType callType;
    //  [Export("callType", ArgumentSemantic.Assign)]
    //  BCXCallType CallType { get; }

    //  // -(BOOL)isAudioVideo;
    //  [Export("isAudioVideo")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioVideo { get; }

    //  // -(BOOL)isAudioUpgradable;
    //  [Export("isAudioUpgradable")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioUpgradable { get; }

    //  // -(BOOL)isAudioOnly;
    //  [Export("isAudioOnly")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioOnly { get; }

    //  // -(BCXCallOptions * _Nonnull)optionsWithRecording:(BOOL)recording;
    //  [Export("optionsWithRecording:")]
    //  BCXCallOptions OptionsWithRecording(bool recording);

    //  // -(BCXCallOptions * _Nonnull)optionsWithDuration:(NSUInteger)duration;
    //  [Export("optionsWithDuration:")]
    //  BCXCallOptions OptionsWithDuration(nuint duration);

    //  // -(BCXCallOptions * _Nonnull)optionsWithCallType:(BCXCallType)callType;
    //  [Export("optionsWithCallType:")]
    //  BCXCallOptions OptionsWithCallType(BCXCallType callType);

    //  // +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording;
    //  [Static]
    //  [Export("optionsWithRecording:")]
    //  BCXCallOptions OptionsWithRecording(bool recording);

    //  // +(instancetype _Nonnull)optionsWithDuration:(NSUInteger)duration;
    //  [Static]
    //  [Export("optionsWithDuration:")]
    //  BCXCallOptions OptionsWithDuration(nuint duration);

    //  // +(instancetype _Nonnull)optionsWithCallType:(BCXCallType)callType;
    //  [Static]
    //  [Export("optionsWithCallType:")]
    //  BCXCallOptions OptionsWithCallType(BCXCallType callType);

    //  // +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording duration:(NSUInteger)duration;
    //  [Static]
    //  [Export("optionsWithRecording:duration:")]
    //  BCXCallOptions OptionsWithRecording(bool recording, nuint duration);

    //  // +(instancetype _Nonnull)optionsWithRecording:(BOOL)recording duration:(NSUInteger)duration callType:(BCXCallType)callType;
    //  [Static]
    //  [Export("optionsWithRecording:duration:callType:")]
    //  BCXCallOptions OptionsWithRecording(bool recording, nuint duration, BCXCallType callType);

    //  // +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withRecording:(BOOL)recording;
    //  [Static]
    //  [Export("optionsFromOptions:withRecording:")]
    //  BCXCallOptions OptionsFromOptions(BCXCallOptions options, bool recording);

    //  // +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withDuration:(NSUInteger)duration;
    //  [Static]
    //  [Export("optionsFromOptions:withDuration:")]
    //  BCXCallOptions OptionsFromOptions(BCXCallOptions options, nuint duration);

    //  // +(instancetype _Nonnull)optionsFromOptions:(BCXCallOptions * _Nonnull)options withCallType:(BCXCallType)callType;
    //  [Static]
    //  [Export("optionsFromOptions:withCallType:")]
    //  BCXCallOptions OptionsFromOptions(BCXCallOptions options, BCXCallType callType);
    //}

    //// @protocol BCXCallObserver <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallObserver
    //{
    //  // @optional -(void)call:(id<BCXCall> _Nonnull)call didChangeState:(BCXCallState)state;
    //  [Export("call:didChangeState:")]
    //  void Call(BCXCall call, BCXCallState state);

    //  // @optional -(void)call:(id<BCXCall> _Nonnull)call didUpdateOptions:(BCXCallOptions * _Nonnull)options;
    //  [Export("call:didUpdateOptions:")]
    //  void Call(BCXCall call, BCXCallOptions options);

    //  // @optional -(void)call:(id<BCXCall> _Nonnull)call didUpdateParticipants:(id<BCXCallParticipants> _Nonnull)participants;
    //  [Export("call:didUpdateParticipants:")]
    //  void Call(BCXCall call, BCXCallParticipants participants);

    //  // @optional -(void)callDidUpgradeToVideoCall:(id<BCXCall> _Nonnull)call;
    //  [Export("callDidUpgradeToVideoCall:")]
    //  void CallDidUpgradeToVideoCall(BCXCall call);

    //  // @optional -(void)callDidConnect:(id<BCXCall> _Nonnull)call;
    //  [Export("callDidConnect:")]
    //  void CallDidConnect(BCXCall call);

    //  // @optional -(void)callDidEnd:(id<BCXCall> _Nonnull)call;
    //  [Export("callDidEnd:")]
    //  void CallDidEnd(BCXCall call);

    //  // @optional -(void)call:(id<BCXCall> _Nonnull)call didFailWithError:(NSError * _Nonnull)error;
    //  [Export("call:didFailWithError:")]
    //  void Call(BCXCall call, NSError error);
    //}

    //// @protocol BCXCall <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCall
    //{
    //  // @required @property (readonly, nonatomic, strong) NSUUID * _Nonnull uuid;
    //  [Abstract]
    //  [Export("uuid", ArgumentSemantic.Strong)]
    //  NSUuid Uuid { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nullable sid;
    //  [Abstract]
    //  [NullAllowed, Export("sid", ArgumentSemantic.Strong)]
    //  string Sid { get; }

    //  // @required @property (readonly, nonatomic, strong) BCXCallOptions * _Nullable options;
    //  [Abstract]
    //  [NullAllowed, Export("options", ArgumentSemantic.Strong)]
    //  BCXCallOptions Options { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXCallEndReason endReason;
    //  [Abstract]
    //  [Export("endReason", ArgumentSemantic.Assign)]
    //  BCXCallEndReason EndReason { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXDeclineReason declineReason;
    //  [Abstract]
    //  [Export("declineReason", ArgumentSemantic.Assign)]
    //  BCXDeclineReason DeclineReason { get; }

    //  // @required @property (readonly, nonatomic, strong) BAVRoom * _Nullable room;
    //  [Abstract]
    //  [NullAllowed, Export("room", ArgumentSemantic.Strong)]
    //  BAVRoom Room { get; }

    //  // @required -(void)addObserver:(id<BCXCallObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
    //  [Abstract]
    //  [Export("addObserver:")]
    //  void AddObserver(BCXCallObserver observer);

    //  // @required -(void)addObserver:(id<BCXCallObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
    //  [Abstract]
    //  [Export("addObserver:queue:")]
    //  void AddObserver(BCXCallObserver observer, [NullAllowed] DispatchQueue queue);

    //  // @required -(void)removeObserver:(id<BCXCallObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
    //  [Abstract]
    //  [Export("removeObserver:")]
    //  void RemoveObserver(BCXCallObserver observer);

    //  // @required @property (readonly, nonatomic, strong) id<BCXCallParticipants> _Nonnull participants;
    //  [Abstract]
    //  [Export("participants", ArgumentSemantic.Strong)]
    //  BCXCallParticipants Participants { get; }

    //  // @required -(BOOL)isGroupCall;
    //  [Abstract]
    //  [Export("isGroupCall")]
    //  [Verify(MethodToProperty)]
    //  bool IsGroupCall { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXCallDirection direction;
    //  [Abstract]
    //  [Export("direction", ArgumentSemantic.Assign)]
    //  BCXCallDirection Direction { get; }

    //  // @required -(BOOL)isIncoming;
    //  [Abstract]
    //  [Export("isIncoming")]
    //  [Verify(MethodToProperty)]
    //  bool IsIncoming { get; }

    //  // @required -(BOOL)isOutgoing;
    //  [Abstract]
    //  [Export("isOutgoing")]
    //  [Verify(MethodToProperty)]
    //  bool IsOutgoing { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXCallType callType;
    //  [Abstract]
    //  [Export("callType", ArgumentSemantic.Assign)]
    //  BCXCallType CallType { get; }

    //  // @required -(BOOL)isAudioVideo;
    //  [Abstract]
    //  [Export("isAudioVideo")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioVideo { get; }

    //  // @required -(BOOL)isAudioUpgradable;
    //  [Abstract]
    //  [Export("isAudioUpgradable")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioUpgradable { get; }

    //  // @required -(BOOL)isAudioOnly;
    //  [Abstract]
    //  [Export("isAudioOnly")]
    //  [Verify(MethodToProperty)]
    //  bool IsAudioOnly { get; }

    //  // @required -(BOOL)canUpgradeToVideo;
    //  [Abstract]
    //  [Export("canUpgradeToVideo")]
    //  [Verify(MethodToProperty)]
    //  bool CanUpgradeToVideo { get; }

    //  // @required -(BOOL)didUpgradeToVideo;
    //  [Abstract]
    //  [Export("didUpgradeToVideo")]
    //  [Verify(MethodToProperty)]
    //  bool DidUpgradeToVideo { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXCallState state;
    //  [Abstract]
    //  [Export("state", ArgumentSemantic.Assign)]
    //  BCXCallState State { get; }

    //  // @required -(BOOL)hasEnded;
    //  [Abstract]
    //  [Export("hasEnded")]
    //  [Verify(MethodToProperty)]
    //  bool HasEnded { get; }

    //  // @required -(BOOL)hasFailed;
    //  [Abstract]
    //  [Export("hasFailed")]
    //  [Verify(MethodToProperty)]
    //  bool HasFailed { get; }

    //  // @required -(BOOL)isIdle;
    //  [Abstract]
    //  [Export("isIdle")]
    //  [Verify(MethodToProperty)]
    //  bool IsIdle { get; }

    //  // @required -(BOOL)isRinging;
    //  [Abstract]
    //  [Export("isRinging")]
    //  [Verify(MethodToProperty)]
    //  bool IsRinging { get; }

    //  // @required -(BOOL)isDialing;
    //  [Abstract]
    //  [Export("isDialing")]
    //  [Verify(MethodToProperty)]
    //  bool IsDialing { get; }

    //  // @required -(BOOL)isConnecting;
    //  [Abstract]
    //  [Export("isConnecting")]
    //  [Verify(MethodToProperty)]
    //  bool IsConnecting { get; }

    //  // @required -(BOOL)isConnected;
    //  [Abstract]
    //  [Export("isConnected")]
    //  [Verify(MethodToProperty)]
    //  bool IsConnected { get; }

    //  // @required -(BOOL)isAnswering;
    //  [Abstract]
    //  [Export("isAnswering")]
    //  [Verify(MethodToProperty)]
    //  bool IsAnswering { get; }

    //  // @required -(BOOL)isDeclining;
    //  [Abstract]
    //  [Export("isDeclining")]
    //  [Verify(MethodToProperty)]
    //  bool IsDeclining { get; }

    //  // @required -(BOOL)isHangingUp;
    //  [Abstract]
    //  [Export("isHangingUp")]
    //  [Verify(MethodToProperty)]
    //  bool IsHangingUp { get; }

    //  // @required @property (getter = isMuted, assign, readwrite, nonatomic) BOOL muted;
    //  [Abstract]
    //  [Export("muted")]
    //  bool Muted { [Bind("isMuted")] get; set; }
    //}

    //// @protocol BCXCallRegistry <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallRegistry
    //{
    //  // @required @property (readonly, copy, nonatomic) NSArray<id<BCXCall>> * _Nonnull calls;
    //  [Abstract]
    //  [Export("calls", ArgumentSemantic.Copy)]
    //  BCXCall[] Calls { get; }

    //  // @required @property (readonly, getter = isBusy, assign, nonatomic) BOOL busy;
    //  [Abstract]
    //  [Export("busy")]
    //  bool Busy { [Bind("isBusy")] get; }

    //  // @required -(id<BCXCall> _Nullable)callWithUUID:(NSUUID * _Nonnull)UUID;
    //  [Abstract]
    //  [Export("callWithUUID:")]
    //  [return: NullAllowed]
    //  BCXCall CallWithUUID(NSUuid UUID);

    //  // @required -(void)addObserver:(id<BCXCallRegistryObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
    //  [Abstract]
    //  [Export("addObserver:")]
    //  void AddObserver(BCXCallRegistryObserver observer);

    //  // @required -(void)addObserver:(id<BCXCallRegistryObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
    //  [Abstract]
    //  [Export("addObserver:queue:")]
    //  void AddObserver(BCXCallRegistryObserver observer, [NullAllowed] DispatchQueue queue);

    //  // @required -(void)removeObserver:(id<BCXCallRegistryObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
    //  [Abstract]
    //  [Export("removeObserver:")]
    //  void RemoveObserver(BCXCallRegistryObserver observer);
    //}

    //// @protocol BCXCallRegistryObserver <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallRegistryObserver
    //{
    //  // @required -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didAddCall:(id<BCXCall> _Nonnull)call;
    //  [Abstract]
    //  [Export("registry:didAddCall:")]
    //  void DidAddCall(BCXCallRegistry registry, BCXCall call);

    //  // @required -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didRemoveCall:(id<BCXCall> _Nonnull)call;
    //  [Abstract]
    //  [Export("registry:didRemoveCall:")]
    //  void DidRemoveCall(BCXCallRegistry registry, BCXCall call);
    //}

    //// @protocol BCXCallParticipantsObserver <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallParticipantsObserver
    //{
    //  // @optional -(void)onCallParticipantStateChanged:(id<BCXCallParticipant> _Nonnull)participant;
    //  [Export("onCallParticipantStateChanged:")]
    //  void OnCallParticipantStateChanged(BCXCallParticipant participant);

    //  // @optional -(void)onCallParticipantUpgradedToVideo:(id<BCXCallParticipant> _Nonnull)participant;
    //  [Export("onCallParticipantUpgradedToVideo:")]
    //  void OnCallParticipantUpgradedToVideo(BCXCallParticipant participant);
    //}

    //// @protocol BDFUser <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BDFUser
    //{
    //  // @required @property (readonly, nonatomic, strong) NSString * _Nonnull alias;
    //  [Abstract]
    //  [Export("alias", ArgumentSemantic.Strong)]
    //  string Alias { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nullable firstName;
    //  [Abstract]
    //  [NullAllowed, Export("firstName", ArgumentSemantic.Strong)]
    //  string FirstName { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nullable lastName;
    //  [Abstract]
    //  [NullAllowed, Export("lastName", ArgumentSemantic.Strong)]
    //  string LastName { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nullable email;
    //  [Abstract]
    //  [NullAllowed, Export("email", ArgumentSemantic.Strong)]
    //  string Email { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nullable imageFilename;
    //  [Abstract]
    //  [NullAllowed, Export("imageFilename", ArgumentSemantic.Strong)]
    //  string ImageFilename { get; }
    //}

    //// @protocol BCXUser <BDFUser>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //interface BCXUser : IBDFUser
    //{
    //  // @required @property (readonly, assign, nonatomic) BCXUserStatus status;
    //  [Abstract]
    //  [Export("status", ArgumentSemantic.Assign)]
    //  BCXUserStatus Status { get; }

    //  // @required -(BOOL)isBusy;
    //  [Abstract]
    //  [Export("isBusy")]
    //  [Verify(MethodToProperty)]
    //  bool IsBusy { get; }

    //  // @required -(BOOL)isOnline;
    //  [Abstract]
    //  [Export("isOnline")]
    //  [Verify(MethodToProperty)]
    //  bool IsOnline { get; }

    //  // @required -(BOOL)isOffline;
    //  [Abstract]
    //  [Export("isOffline")]
    //  [Verify(MethodToProperty)]
    //  bool IsOffline { get; }

    //  // @required -(BOOL)canUpgradeToVideo;
    //  [Abstract]
    //  [Export("canUpgradeToVideo")]
    //  [Verify(MethodToProperty)]
    //  bool CanUpgradeToVideo { get; }
    //}

    //// @protocol BCXCallParticipant <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallParticipant
    //{
    //  // @required @property (readonly, nonatomic, strong) id<BCXUser> _Nonnull user;
    //  [Abstract]
    //  [Export("user", ArgumentSemantic.Strong)]
    //  BCXUser User { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nonnull userId;
    //  [Abstract]
    //  [Export("userId", ArgumentSemantic.Strong)]
    //  string UserId { get; }

    //  // @required @property (readonly, assign, nonatomic) BCXCallParticipantState state;
    //  [Abstract]
    //  [Export("state", ArgumentSemantic.Assign)]
    //  BCXCallParticipantState State { get; }

    //  // @required -(BOOL)hasAnswered;
    //  [Abstract]
    //  [Export("hasAnswered")]
    //  [Verify(MethodToProperty)]
    //  bool HasAnswered { get; }

    //  // @required -(BOOL)hasDeclined;
    //  [Abstract]
    //  [Export("hasDeclined")]
    //  [Verify(MethodToProperty)]
    //  bool HasDeclined { get; }

    //  // @required -(BOOL)hasDeclinedByDoNotDisturb;
    //  [Abstract]
    //  [Export("hasDeclinedByDoNotDisturb")]
    //  [Verify(MethodToProperty)]
    //  bool HasDeclinedByDoNotDisturb { get; }

    //  // @required -(BOOL)didNotAnswer;
    //  [Abstract]
    //  [Export("didNotAnswer")]
    //  [Verify(MethodToProperty)]
    //  bool DidNotAnswer { get; }

    //  // @required -(BOOL)hasTimedOut;
    //  [Abstract]
    //  [Export("hasTimedOut")]
    //  [Verify(MethodToProperty)]
    //  bool HasTimedOut { get; }

    //  // @required -(BOOL)hasDisconnected;
    //  [Abstract]
    //  [Export("hasDisconnected")]
    //  [Verify(MethodToProperty)]
    //  bool HasDisconnected { get; }

    //  // @required -(BOOL)didUpgradeToVideo;
    //  [Abstract]
    //  [Export("didUpgradeToVideo")]
    //  [Verify(MethodToProperty)]
    //  bool DidUpgradeToVideo { get; }
    //}

    //// @protocol BCXCallParticipants <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXCallParticipants
    //{
    //  // @required @property (readonly, nonatomic, strong) id<BCXCallParticipant> _Nonnull caller;
    //  [Abstract]
    //  [Export("caller", ArgumentSemantic.Strong)]
    //  BCXCallParticipant Caller { get; }

    //  // @required @property (readonly, nonatomic, strong) NSString * _Nonnull callerId;
    //  [Abstract]
    //  [Export("callerId", ArgumentSemantic.Strong)]
    //  string CallerId { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull callees;
    //  [Abstract]
    //  [Export("callees", ArgumentSemantic.Strong)]
    //  BCXCallParticipant[] Callees { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull calleeIds;
    //  [Abstract]
    //  [Export("calleeIds", ArgumentSemantic.Strong)]
    //  string[] CalleeIds { get; }

    //  // @required -(id<BCXCallParticipant> _Nullable)calleeWithIdentifier:(NSString * _Nonnull)identifier;
    //  [Abstract]
    //  [Export("calleeWithIdentifier:")]
    //  [return: NullAllowed]
    //  BCXCallParticipant CalleeWithIdentifier(string identifier);

    //  // @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull allParticipants;
    //  [Abstract]
    //  [Export("allParticipants", ArgumentSemantic.Strong)]
    //  BCXCallParticipant[] AllParticipants { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<id<BCXCallParticipant>> * _Nonnull opponents;
    //  [Abstract]
    //  [Export("opponents", ArgumentSemantic.Strong)]
    //  BCXCallParticipant[] Opponents { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * _Nonnull participantsIds;
    //  [Abstract]
    //  [Export("participantsIds", ArgumentSemantic.Strong)]
    //  string[] ParticipantsIds { get; }

    //  // @required -(id<BCXCallParticipant> _Nullable)participantWithIdentifier:(NSString * _Nonnull)identifier;
    //  [Abstract]
    //  [Export("participantWithIdentifier:")]
    //  [return: NullAllowed]
    //  BCXCallParticipant ParticipantWithIdentifier(string identifier);

    //  // @required -(id<BCXCallParticipant> _Nullable)authenticatedUserParticipant;
    //  [Abstract]
    //  [NullAllowed, Export("authenticatedUserParticipant")]
    //  [Verify(MethodToProperty)]
    //  BCXCallParticipant AuthenticatedUserParticipant { get; }

    //  // @required -(BOOL)isAuthenticatedUserTheCaller;
    //  [Abstract]
    //  [Export("isAuthenticatedUserTheCaller")]
    //  [Verify(MethodToProperty)]
    //  bool IsAuthenticatedUserTheCaller { get; }

    //  // @required -(void)addObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
    //  [Abstract]
    //  [Export("addObserver:")]
    //  void AddObserver(BCXCallParticipantsObserver observer);

    //  // @required -(void)addObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
    //  [Abstract]
    //  [Export("addObserver:queue:")]
    //  void AddObserver(BCXCallParticipantsObserver observer, [NullAllowed] DispatchQueue queue);

    //  // @required -(void)removeObserver:(id<BCXCallParticipantsObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
    //  [Abstract]
    //  [Export("removeObserver:")]
    //  void RemoveObserver(BCXCallParticipantsObserver observer);

    //  // @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)busy;
    //  [Abstract]
    //  [Export("busy")]
    //  [Verify(MethodToProperty)]
    //  BCXCallParticipant[] Busy { get; }

    //  // @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)online;
    //  [Abstract]
    //  [Export("online")]
    //  [Verify(MethodToProperty)]
    //  BCXCallParticipant[] Online { get; }

    //  // @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)offline;
    //  [Abstract]
    //  [Export("offline")]
    //  [Verify(MethodToProperty)]
    //  BCXCallParticipant[] Offline { get; }

    //  // @required -(BOOL)hasEverybodyDeclined;
    //  [Abstract]
    //  [Export("hasEverybodyDeclined")]
    //  [Verify(MethodToProperty)]
    //  bool HasEverybodyDeclined { get; }

    //  // @required -(BOOL)hasAnybodyAnswered;
    //  [Abstract]
    //  [Export("hasAnybodyAnswered")]
    //  [Verify(MethodToProperty)]
    //  bool HasAnybodyAnswered { get; }

    //  // @required -(BOOL)hasAnybodyUpgradedToVideo;
    //  [Abstract]
    //  [Export("hasAnybodyUpgradedToVideo")]
    //  [Verify(MethodToProperty)]
    //  bool HasAnybodyUpgradedToVideo { get; }

    //  // @required -(NSArray<id<BCXCallParticipant>> * _Nonnull)upgradedToVideo;
    //  [Abstract]
    //  [Export("upgradedToVideo")]
    //  [Verify(MethodToProperty)]
    //  BCXCallParticipant[] UpgradedToVideo { get; }
    //}

    //// @protocol BCXAction <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXAction
    //{
    //  // @required @property (readonly, getter = isCompleted, assign, nonatomic) BOOL completed;
    //  [Abstract]
    //  [Export("completed")]
    //  bool Completed { [Bind("isCompleted")] get; }

    //  // @required @property (readonly, getter = isFaulted, assign, nonatomic) BOOL faulted;
    //  [Abstract]
    //  [Export("faulted")]
    //  bool Faulted { [Bind("isFaulted")] get; }

    //  // @required @property (readonly, nonatomic, strong) id _Nullable result;
    //  [Abstract]
    //  [NullAllowed, Export("result", ArgumentSemantic.Strong)]
    //  NSObject Result { get; }

    //  // @required @property (readonly, nonatomic, strong) NSError * _Nullable error;
    //  [Abstract]
    //  [NullAllowed, Export("error", ArgumentSemantic.Strong)]
    //  NSError Error { get; }

    //  // @required -(void)fulfill;
    //  [Abstract]
    //  [Export("fulfill")]
    //  void Fulfill();

    //  // @required -(void)fulfillWithResult:(id _Nullable)result;
    //  [Abstract]
    //  [Export("fulfillWithResult:")]
    //  void FulfillWithResult([NullAllowed] NSObject result);

    //  // @required -(void)failWithError:(NSError * _Nonnull)error;
    //  [Abstract]
    //  [Export("failWithError:")]
    //  void FailWithError(NSError error);

    //  // @required -(void)continueWith:(void (^ _Nonnull)(id<BCXAction> _Nonnull))block;
    //  [Abstract]
    //  [Export("continueWith:")]
    //  void ContinueWith(Action<BCXAction> block);

    //  // @required -(void)continueOnMainQueueWith:(void (^ _Nonnull)(id<BCXAction> _Nonnull))block;
    //  [Abstract]
    //  [Export("continueOnMainQueueWith:")]
    //  void ContinueOnMainQueueWith(Action<BCXAction> block);

    //  // @required -(void)continueOnQueue:(dispatch_queue_t _Nonnull)queue with:(void (^ _Nonnull)(id<BCXAction> _Nonnull))block;
    //  [Abstract]
    //  [Export("continueOnQueue:with:")]
    //  void ContinueOnQueue(DispatchQueue queue, Action<BCXAction> block);
    //}

    //[Static]
    //[Verify(ConstantsInterfaceAssociation)]
    //partial interface Constants
    //{
    //  // extern NSString *const _Nonnull kBCXErrorDomain;
    //  [Field("kBCXErrorDomain", "__Internal")]
    //  NSString kBCXErrorDomain { get; }
    //}

    //// @interface BCXError : NSError
    //[BaseType(typeof(NSError))]
    //[DisableDefaultCtor]
    //interface BCXError
    //{
    //}

    // @protocol BCHChatClientObserver <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface BCHChatClientObserver
    {
        // @optional -(void)chatClientWillStart:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientWillStart:")]
        void ChatClientWillStart(BCHChatClient client);

        // @optional -(void)chatClientDidStart:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientDidStart:")]
        void ChatClientDidStart(BCHChatClient client);

        // @optional -(void)chatClientWillPause:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientWillPause:")]
        void ChatClientWillPause(BCHChatClient client);

        // @optional -(void)chatClientDidPause:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientDidPause:")]
        void ChatClientDidPause(BCHChatClient client);

        // @optional -(void)chatClientWillStop:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientWillStop:")]
        void ChatClientWillStop(BCHChatClient client);

        // @optional -(void)chatClientDidStop:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientDidStop:")]
        void ChatClientDidStop(BCHChatClient client);

        // @optional -(void)chatClientWillResume:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientWillResume:")]
        void ChatClientWillResume(BCHChatClient client);

        // @optional -(void)chatClientDidResume:(id<BCHChatClient> _Nonnull)client;
        [Export("chatClientDidResume:")]
        void ChatClientDidResume(BCHChatClient client);

        // @optional -(void)chatClient:(id<BCHChatClient> _Nonnull)client didFailWithError:(NSError * _Nonnull)error;
        [Export("chatClient:didFailWithError:")]
        void ChatClientDidFailWithError(BCHChatClient client, NSError error);
    }

    interface IBCHChatClientObserver { }

    // @protocol BCHChatClient <NSObject>
    /*
      Check whether adding [Model] to this declaration is appropriate.
      [Model] is used to generate a C# class that implements this protocol,
      and might be useful for protocols that consumers are supposed to implement,
      since consumers can subclass the generated class instead of implementing
      the generated interface. If consumers are not supposed to implement this
      protocol, then [Model] is redundant and will generate code that will never
      be used.
    */
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface BCHChatClient
    {
        // @required @property (readonly, assign, nonatomic) BCHChatClientState state;
        [Abstract]
        [Export("state", ArgumentSemantic.Assign)]
        BCHChatClientState State { get; }

        // @required -(void)addObserver:(id<BCHChatClientObserver> _Nonnull)observer __attribute__((swift_name("add(observer:)")));
        [Abstract]
        [Export("addObserver:")]
        void AddObserver(IBCHChatClientObserver observer);

        // @required -(void)addObserver:(id<BCHChatClientObserver> _Nonnull)observer queue:(dispatch_queue_t _Nullable)queue __attribute__((swift_name("add(observer:queue:)")));
        [Abstract]
        [Export("addObserver:queue:")]
        void AddObserver(IBCHChatClientObserver observer, [NullAllowed] DispatchQueue queue);

        // @required -(void)removeObserver:(id<BCHChatClientObserver> _Nonnull)observer __attribute__((swift_name("remove(observer:)")));
        [Abstract]
        [Export("removeObserver:")]
        void RemoveObserver(IBCHChatClientObserver observer);

        // @required -(void)start:(NSString * _Nonnull)userId __attribute__((swift_name("start(userId:)")));
        [Abstract]
        [Export("start:")]
        void Start(string userId);

        // @required -(void)resume;
        [Abstract]
        [Export("resume")]
        void Resume();

        // @required -(void)pause;
        [Abstract]
        [Export("pause")]
        void Pause();

        // @required -(void)stop;
        [Abstract]
        [Export("stop")]
        void Stop();
    }

    interface IBCHChatClient { }

    //// @interface BCHChatNotification : NSObject
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BCHChatNotification
    //{
    //}

    //// @protocol BCXHandleProvider <NSObject, NSCopying>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXHandleProvider : INSCopying
    //{
    //  // @required -(void)handleForAliases:(NSArray<NSString *> * _Nullable)aliases completion:(void (^ _Nonnull)(CXHandle * _Nonnull))completion __attribute__((availability(ios, introduced=10.0)));
    //  [iOS(10, 0)]
    //  [Abstract]
    //  [Export("handleForAliases:completion:")]
    //  void Completion([NullAllowed] string[] aliases, Action<CXHandle> completion);
    //}

    //// @interface BCXAdditions (PKPushCredentials)
    //[Category]
    //[BaseType(typeof(PKPushCredentials))]
    //interface PKPushCredentials_BCXAdditions
    //{
    //  // @property (readonly, copy, nonatomic) NSString * _Nullable bcx_tokenAsString __attribute__((swift_name("tokenAsString")));
    //  [NullAllowed, Export("bcx_tokenAsString")]
    //  string Bcx_tokenAsString { get; }
    //}

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

    //// @interface BDFDDLog : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLog
    //{
    //  // @property (readonly, nonatomic, strong, class) BDFDDLog * _Nonnull sharedInstance;
    //  [Static]
    //  [Export("sharedInstance", ArgumentSemantic.Strong)]
    //  BDFDDLog SharedInstance { get; }

    //  // @property (readonly, nonatomic, strong, class) dispatch_queue_t _Nonnull loggingQueue;
    //  [Static]
    //  [Export("loggingQueue", ArgumentSemantic.Strong)]
    //  DispatchQueue LoggingQueue { get; }

    //  // +(void)log:(BOOL)asynchronous level:(BDFDDLogLevel)level flag:(BDFDDLogFlag)flag context:(NSInteger)context file:(const char * _Nonnull)file function:(const char * _Nonnull)function line:(NSUInteger)line tag:(id _Nullable)tag format:(NSString * _Nonnull)format, ... __attribute__((format(NSString, 9, 10)));
    //  [Static, Internal]
    //  [Export("log:level:flag:context:file:function:line:tag:format:", IsVariadic = true)]
    //  unsafe void Log(bool asynchronous, BDFDDLogLevel level, BDFDDLogFlag flag, nint context, sbyte* file, sbyte* function, nuint line, [NullAllowed] NSObject tag, string format, IntPtr varArgs);

    //  // -(void)log:(BOOL)asynchronous level:(BDFDDLogLevel)level flag:(BDFDDLogFlag)flag context:(NSInteger)context file:(const char * _Nonnull)file function:(const char * _Nonnull)function line:(NSUInteger)line tag:(id _Nullable)tag format:(NSString * _Nonnull)format, ... __attribute__((format(NSString, 9, 10)));
    //  [Internal]
    //  [Export("log:level:flag:context:file:function:line:tag:format:", IsVariadic = true)]
    //  unsafe void Log(bool asynchronous, BDFDDLogLevel level, BDFDDLogFlag flag, nint context, sbyte* file, sbyte* function, nuint line, [NullAllowed] NSObject tag, string format, IntPtr varArgs);

    //  // +(void)log:(BOOL)asynchronous level:(BDFDDLogLevel)level flag:(BDFDDLogFlag)flag context:(NSInteger)context file:(const char * _Nonnull)file function:(const char * _Nonnull)function line:(NSUInteger)line tag:(id _Nullable)tag format:(NSString * _Nonnull)format args:(va_list)argList __attribute__((swift_name("log(asynchronous:level:flag:context:file:function:line:tag:format:arguments:)")));
    //  [Static]
    //  [Export("log:level:flag:context:file:function:line:tag:format:args:")]
    //  unsafe void Log(bool asynchronous, BDFDDLogLevel level, BDFDDLogFlag flag, nint context, sbyte* file, sbyte* function, nuint line, [NullAllowed] NSObject tag, string format, sbyte* argList);

    //  // -(void)log:(BOOL)asynchronous level:(BDFDDLogLevel)level flag:(BDFDDLogFlag)flag context:(NSInteger)context file:(const char * _Nonnull)file function:(const char * _Nonnull)function line:(NSUInteger)line tag:(id _Nullable)tag format:(NSString * _Nonnull)format args:(va_list)argList __attribute__((swift_name("log(asynchronous:level:flag:context:file:function:line:tag:format:arguments:)")));
    //  [Export("log:level:flag:context:file:function:line:tag:format:args:")]
    //  unsafe void Log(bool asynchronous, BDFDDLogLevel level, BDFDDLogFlag flag, nint context, sbyte* file, sbyte* function, nuint line, [NullAllowed] NSObject tag, string format, sbyte* argList);

    //  // +(void)log:(BOOL)asynchronous message:(BDFDDLogMessage * _Nonnull)logMessage __attribute__((swift_name("log(asynchronous:message:)")));
    //  [Static]
    //  [Export("log:message:")]
    //  void Log(bool asynchronous, BDFDDLogMessage logMessage);

    //  // -(void)log:(BOOL)asynchronous message:(BDFDDLogMessage * _Nonnull)logMessage __attribute__((swift_name("log(asynchronous:message:)")));
    //  [Export("log:message:")]
    //  void Log(bool asynchronous, BDFDDLogMessage logMessage);

    //  // +(void)flushLog;
    //  [Static]
    //  [Export("flushLog")]
    //  void FlushLog();

    //  // -(void)flushLog;
    //  [Export("flushLog")]
    //  void FlushLog();

    //  // +(void)addLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Static]
    //  [Export("addLogger:")]
    //  void AddLogger(BDFDDLogger logger);

    //  // -(void)addLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Export("addLogger:")]
    //  void AddLogger(BDFDDLogger logger);

    //  // +(void)addLogger:(id<BDFDDLogger> _Nonnull)logger withLevel:(BDFDDLogLevel)level;
    //  [Static]
    //  [Export("addLogger:withLevel:")]
    //  void AddLogger(BDFDDLogger logger, BDFDDLogLevel level);

    //  // -(void)addLogger:(id<BDFDDLogger> _Nonnull)logger withLevel:(BDFDDLogLevel)level;
    //  [Export("addLogger:withLevel:")]
    //  void AddLogger(BDFDDLogger logger, BDFDDLogLevel level);

    //  // +(void)removeLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Static]
    //  [Export("removeLogger:")]
    //  void RemoveLogger(BDFDDLogger logger);

    //  // -(void)removeLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Export("removeLogger:")]
    //  void RemoveLogger(BDFDDLogger logger);

    //  // +(void)removeAllLoggers;
    //  [Static]
    //  [Export("removeAllLoggers")]
    //  void RemoveAllLoggers();

    //  // -(void)removeAllLoggers;
    //  [Export("removeAllLoggers")]
    //  void RemoveAllLoggers();

    //  // @property (readonly, copy, nonatomic, class) NSArray<id<BDFDDLogger>> * _Nonnull allLoggers;
    //  [Static]
    //  [Export("allLoggers", ArgumentSemantic.Copy)]
    //  BDFDDLogger[] AllLoggers { get; }

    //  // @property (readonly, copy, nonatomic) NSArray<id<BDFDDLogger>> * _Nonnull allLoggers;
    //  [Export("allLoggers", ArgumentSemantic.Copy)]
    //  BDFDDLogger[] AllLoggers { get; }

    //  // @property (readonly, copy, nonatomic, class) NSArray<BDFDDLoggerInformation *> * _Nonnull allLoggersWithLevel;
    //  [Static]
    //  [Export("allLoggersWithLevel", ArgumentSemantic.Copy)]
    //  BDFDDLoggerInformation[] AllLoggersWithLevel { get; }

    //  // @property (readonly, copy, nonatomic) NSArray<BDFDDLoggerInformation *> * _Nonnull allLoggersWithLevel;
    //  [Export("allLoggersWithLevel", ArgumentSemantic.Copy)]
    //  BDFDDLoggerInformation[] AllLoggersWithLevel { get; }

    //  // @property (readonly, copy, nonatomic, class) NSArray<Class> * _Nonnull registeredClasses;
    //  [Static]
    //  [Export("registeredClasses", ArgumentSemantic.Copy)]
    //  Class[] RegisteredClasses { get; }

    //  // @property (readonly, copy, nonatomic, class) NSArray<NSString *> * _Nonnull registeredClassNames;
    //  [Static]
    //  [Export("registeredClassNames", ArgumentSemantic.Copy)]
    //  string[] RegisteredClassNames { get; }

    //  // +(BDFDDLogLevel)levelForClass:(Class _Nonnull)aClass;
    //  [Static]
    //  [Export("levelForClass:")]
    //  BDFDDLogLevel LevelForClass(Class aClass);

    //  // +(BDFDDLogLevel)levelForClassWithName:(NSString * _Nonnull)aClassName;
    //  [Static]
    //  [Export("levelForClassWithName:")]
    //  BDFDDLogLevel LevelForClassWithName(string aClassName);

    //  // +(void)setLevel:(BDFDDLogLevel)level forClass:(Class _Nonnull)aClass;
    //  [Static]
    //  [Export("setLevel:forClass:")]
    //  void SetLevel(BDFDDLogLevel level, Class aClass);

    //  // +(void)setLevel:(BDFDDLogLevel)level forClassWithName:(NSString * _Nonnull)aClassName;
    //  [Static]
    //  [Export("setLevel:forClassWithName:")]
    //  void SetLevel(BDFDDLogLevel level, string aClassName);
    //}

    //// @protocol BDFDDLogger <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogger
    //{
    //  // @required -(void)logMessage:(BDFDDLogMessage * _Nonnull)logMessage __attribute__((swift_name("log(message:)")));
    //  [Abstract]
    //  [Export("logMessage:")]
    //  void LogMessage(BDFDDLogMessage logMessage);

    //  // @required @property (nonatomic, strong) id<BDFDDLogFormatter> _Nonnull logFormatter;
    //  [Abstract]
    //  [Export("logFormatter", ArgumentSemantic.Strong)]
    //  BDFDDLogFormatter LogFormatter { get; set; }

    //  // @optional -(void)didAddLogger;
    //  [Export("didAddLogger")]
    //  void DidAddLogger();

    //  // @optional -(void)didAddLoggerInQueue:(dispatch_queue_t _Nonnull)queue;
    //  [Export("didAddLoggerInQueue:")]
    //  void DidAddLoggerInQueue(DispatchQueue queue);

    //  // @optional -(void)willRemoveLogger;
    //  [Export("willRemoveLogger")]
    //  void WillRemoveLogger();

    //  // @optional -(void)flush;
    //  [Export("flush")]
    //  void Flush();

    //  // @optional @property (readonly, nonatomic, strong) dispatch_queue_t _Nonnull loggerQueue;
    //  [Export("loggerQueue", ArgumentSemantic.Strong)]
    //  DispatchQueue LoggerQueue { get; }

    //  // @optional @property (readonly, nonatomic) NSString * _Nonnull loggerName;
    //  [Export("loggerName")]
    //  string LoggerName { get; }
    //}

    //// @protocol BDFDDLogFormatter <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogFormatter
    //{
    //  // @required -(NSString * _Nullable)formatLogMessage:(BDFDDLogMessage * _Nonnull)logMessage __attribute__((swift_name("format(message:)")));
    //  [Abstract]
    //  [Export("formatLogMessage:")]
    //  [return: NullAllowed]
    //  string FormatLogMessage(BDFDDLogMessage logMessage);

    //  // @optional -(void)didAddToLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Export("didAddToLogger:")]
    //  void DidAddToLogger(BDFDDLogger logger);

    //  // @optional -(void)didAddToLogger:(id<BDFDDLogger> _Nonnull)logger inQueue:(dispatch_queue_t _Nonnull)queue;
    //  [Export("didAddToLogger:inQueue:")]
    //  void DidAddToLogger(BDFDDLogger logger, DispatchQueue queue);

    //  // @optional -(void)willRemoveFromLogger:(id<BDFDDLogger> _Nonnull)logger;
    //  [Export("willRemoveFromLogger:")]
    //  void WillRemoveFromLogger(BDFDDLogger logger);
    //}

    //// @protocol BDFDDRegisteredDynamicLogging
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //interface BDFDDRegisteredDynamicLogging
    //{
    //  // @required @property (readwrite, nonatomic, setter = ddSetLogLevel:, class) BDFDDLogLevel ddLogLevel;
    //  [Static, Abstract]
    //  [Export("ddLogLevel", ArgumentSemantic.Assign)]
    //  BDFDDLogLevel DdLogLevel { get; [Bind("ddSetLogLevel:")] set; }
    //}

    //// @interface BDFDDLogMessage : NSObject <NSCopying>
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogMessage : INSCopying
    //{
    //  // -(instancetype _Nonnull)initWithMessage:(NSString * _Nonnull)message level:(BDFDDLogLevel)level flag:(BDFDDLogFlag)flag context:(NSInteger)context file:(NSString * _Nonnull)file function:(NSString * _Nullable)function line:(NSUInteger)line tag:(id _Nullable)tag options:(BDFDDLogMessageOptions)options timestamp:(NSDate * _Nullable)timestamp __attribute__((objc_designated_initializer));
    //  [Export("initWithMessage:level:flag:context:file:function:line:tag:options:timestamp:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(string message, BDFDDLogLevel level, BDFDDLogFlag flag, nint context, string file, [NullAllowed] string function, nuint line, [NullAllowed] NSObject tag, BDFDDLogMessageOptions options, [NullAllowed] NSDate timestamp);

    //  // @property (readonly, nonatomic) NSString * _Nonnull message;
    //  [Export("message")]
    //  string Message { get; }

    //  // @property (readonly, nonatomic) BDFDDLogLevel level;
    //  [Export("level")]
    //  BDFDDLogLevel Level { get; }

    //  // @property (readonly, nonatomic) BDFDDLogFlag flag;
    //  [Export("flag")]
    //  BDFDDLogFlag Flag { get; }

    //  // @property (readonly, nonatomic) NSInteger context;
    //  [Export("context")]
    //  nint Context { get; }

    //  // @property (readonly, nonatomic) NSString * _Nonnull file;
    //  [Export("file")]
    //  string File { get; }

    //  // @property (readonly, nonatomic) NSString * _Nonnull fileName;
    //  [Export("fileName")]
    //  string FileName { get; }

    //  // @property (readonly, nonatomic) NSString * _Nullable function;
    //  [NullAllowed, Export("function")]
    //  string Function { get; }

    //  // @property (readonly, nonatomic) NSUInteger line;
    //  [Export("line")]
    //  nuint Line { get; }

    //  // @property (readonly, nonatomic) id _Nullable tag;
    //  [NullAllowed, Export("tag")]
    //  NSObject Tag { get; }

    //  // @property (readonly, nonatomic) BDFDDLogMessageOptions options;
    //  [Export("options")]
    //  BDFDDLogMessageOptions Options { get; }

    //  // @property (readonly, nonatomic) NSDate * _Nonnull timestamp;
    //  [Export("timestamp")]
    //  NSDate Timestamp { get; }

    //  // @property (readonly, nonatomic) NSString * _Nonnull threadID;
    //  [Export("threadID")]
    //  string ThreadID { get; }

    //  // @property (readonly, nonatomic) NSString * _Nonnull threadName;
    //  [Export("threadName")]
    //  string ThreadName { get; }

    //  // @property (readonly, nonatomic) NSString * _Nonnull queueLabel;
    //  [Export("queueLabel")]
    //  string QueueLabel { get; }
    //}

    //// @interface BDFDDAbstractLogger : NSObject <BDFDDLogger>
    //[BaseType(typeof(NSObject))]
    //interface BDFDDAbstractLogger : IBDFDDLogger
    //{
    //  // @property (nonatomic, strong) id<BDFDDLogFormatter> _Nullable logFormatter;
    //  [NullAllowed, Export("logFormatter", ArgumentSemantic.Strong)]
    //  BDFDDLogFormatter LogFormatter { get; set; }

    //  // @property (nonatomic, strong) dispatch_queue_t _Nonnull loggerQueue;
    //  [Export("loggerQueue", ArgumentSemantic.Strong)]
    //  DispatchQueue LoggerQueue { get; set; }

    //  // @property (readonly, getter = isOnGlobalLoggingQueue, nonatomic) BOOL onGlobalLoggingQueue;
    //  [Export("onGlobalLoggingQueue")]
    //  bool OnGlobalLoggingQueue { [Bind("isOnGlobalLoggingQueue")] get; }

    //  // @property (readonly, getter = isOnInternalLoggerQueue, nonatomic) BOOL onInternalLoggerQueue;
    //  [Export("onInternalLoggerQueue")]
    //  bool OnInternalLoggerQueue { [Bind("isOnInternalLoggerQueue")] get; }
    //}

    //// @interface BDFDDLoggerInformation : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLoggerInformation
    //{
    //  // @property (readonly, nonatomic) id<BDFDDLogger> _Nonnull logger;
    //  [Export("logger")]
    //  BDFDDLogger Logger { get; }

    //  // @property (readonly, nonatomic) BDFDDLogLevel level;
    //  [Export("level")]
    //  BDFDDLogLevel Level { get; }

    //  // +(BDFDDLoggerInformation * _Nonnull)informationWithLogger:(id<BDFDDLogger> _Nonnull)logger andLevel:(BDFDDLogLevel)level;
    //  [Static]
    //  [Export("informationWithLogger:andLevel:")]
    //  BDFDDLoggerInformation InformationWithLogger(BDFDDLogger logger, BDFDDLogLevel level);
    //}

    //[Static]
    //[Verify(ConstantsInterfaceAssociation)]
    //partial interface Constants
    //{
    //  // extern const char *const kBDFDDASLKeyDDLog;
    //  [Field("kBDFDDASLKeyDDLog", "__Internal")]
    //  unsafe sbyte* kBDFDDASLKeyDDLog { get; }

    //  // extern const char *const kBDFDDASLDDLogValue;
    //  [Field("kBDFDDASLDDLogValue", "__Internal")]
    //  unsafe sbyte* kBDFDDASLDDLogValue { get; }
    //}

    //// @interface BDFDDASLLogger : BDFDDAbstractLogger <BDFDDLogger>
    //[BaseType(typeof(BDFDDAbstractLogger))]
    //interface BDFDDASLLogger : IBDFDDLogger
    //{
    //  // @property (readonly, strong, class) BDFDDASLLogger * sharedInstance;
    //  [Static]
    //  [Export("sharedInstance", ArgumentSemantic.Strong)]
    //  BDFDDASLLogger SharedInstance { get; }
    //}

    //// @interface BDFDDASLLogCapture : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BDFDDASLLogCapture
    //{
    //  // +(void)start;
    //  [Static]
    //  [Export("start")]
    //  void Start();

    //  // +(void)stop;
    //  [Static]
    //  [Export("stop")]
    //  void Stop();

    //  // @property (class) BDFDDLogLevel captureLevel;
    //  [Static]
    //  [Export("captureLevel", ArgumentSemantic.Assign)]
    //  BDFDDLogLevel CaptureLevel { get; set; }
    //}

    //// @interface BDFDDTTYLogger : BDFDDAbstractLogger <BDFDDLogger>
    //[BaseType(typeof(BDFDDAbstractLogger))]
    //interface BDFDDTTYLogger : IBDFDDLogger
    //{
    //  // @property (readonly, strong, class) BDFDDTTYLogger * sharedInstance;
    //  [Static]
    //  [Export("sharedInstance", ArgumentSemantic.Strong)]
    //  BDFDDTTYLogger SharedInstance { get; }

    //  // @property (assign, readwrite) BOOL colorsEnabled;
    //  [Export("colorsEnabled")]
    //  bool ColorsEnabled { get; set; }

    //  // @property (assign, readwrite, nonatomic) BOOL automaticallyAppendNewlineForCustomFormatters;
    //  [Export("automaticallyAppendNewlineForCustomFormatters")]
    //  bool AutomaticallyAppendNewlineForCustomFormatters { get; set; }

    //  // -(void)setForegroundColor:(BDFDDColor *)txtColor backgroundColor:(BDFDDColor *)bgColor forFlag:(BDFDDLogFlag)mask;
    //  [Export("setForegroundColor:backgroundColor:forFlag:")]
    //  void SetForegroundColor(UIColor txtColor, UIColor bgColor, BDFDDLogFlag mask);

    //  // -(void)setForegroundColor:(BDFDDColor *)txtColor backgroundColor:(BDFDDColor *)bgColor forFlag:(BDFDDLogFlag)mask context:(NSInteger)ctxt;
    //  [Export("setForegroundColor:backgroundColor:forFlag:context:")]
    //  void SetForegroundColor(UIColor txtColor, UIColor bgColor, BDFDDLogFlag mask, nint ctxt);

    //  // -(void)setForegroundColor:(BDFDDColor *)txtColor backgroundColor:(BDFDDColor *)bgColor forTag:(id<NSCopying>)tag;
    //  [Export("setForegroundColor:backgroundColor:forTag:")]
    //  void SetForegroundColor(UIColor txtColor, UIColor bgColor, NSCopying tag);

    //  // -(void)clearColorsForFlag:(BDFDDLogFlag)mask;
    //  [Export("clearColorsForFlag:")]
    //  void ClearColorsForFlag(BDFDDLogFlag mask);

    //  // -(void)clearColorsForFlag:(BDFDDLogFlag)mask context:(NSInteger)context;
    //  [Export("clearColorsForFlag:context:")]
    //  void ClearColorsForFlag(BDFDDLogFlag mask, nint context);

    //  // -(void)clearColorsForTag:(id<NSCopying>)tag;
    //  [Export("clearColorsForTag:")]
    //  void ClearColorsForTag(NSCopying tag);

    //  // -(void)clearColorsForAllFlags;
    //  [Export("clearColorsForAllFlags")]
    //  void ClearColorsForAllFlags();

    //  // -(void)clearColorsForAllTags;
    //  [Export("clearColorsForAllTags")]
    //  void ClearColorsForAllTags();

    //  // -(void)clearAllColors;
    //  [Export("clearAllColors")]
    //  void ClearAllColors();
    //}

    //[Static]
    //[Verify(ConstantsInterfaceAssociation)]
    //partial interface Constants
    //{
    //  // extern const unsigned long long kBDFDDDefaultLogMaxFileSize;
    //  [Field("kBDFDDDefaultLogMaxFileSize", "__Internal")]
    //  ulong kBDFDDDefaultLogMaxFileSize { get; }

    //  // extern const NSTimeInterval kBDFDDDefaultLogRollingFrequency;
    //  [Field("kBDFDDDefaultLogRollingFrequency", "__Internal")]
    //  double kBDFDDDefaultLogRollingFrequency { get; }

    //  // extern const NSUInteger kBDFDDDefaultLogMaxNumLogFiles;
    //  [Field("kBDFDDDefaultLogMaxNumLogFiles", "__Internal")]
    //  nuint kBDFDDDefaultLogMaxNumLogFiles { get; }

    //  // extern const unsigned long long kBDFDDDefaultLogFilesDiskQuota;
    //  [Field("kBDFDDDefaultLogFilesDiskQuota", "__Internal")]
    //  ulong kBDFDDDefaultLogFilesDiskQuota { get; }
    //}

    //// @protocol BDFDDLogFileManager <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogFileManager
    //{
    //  // @required @property (assign, readwrite, atomic) NSUInteger maximumNumberOfLogFiles;
    //  [Abstract]
    //  [Export("maximumNumberOfLogFiles")]
    //  nuint MaximumNumberOfLogFiles { get; set; }

    //  // @required @property (assign, readwrite, atomic) unsigned long long logFilesDiskQuota;
    //  [Abstract]
    //  [Export("logFilesDiskQuota")]
    //  ulong LogFilesDiskQuota { get; set; }

    //  // @required @property (readonly, copy, nonatomic) NSString * logsDirectory;
    //  [Abstract]
    //  [Export("logsDirectory")]
    //  string LogsDirectory { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * unsortedLogFilePaths;
    //  [Abstract]
    //  [Export("unsortedLogFilePaths", ArgumentSemantic.Strong)]
    //  string[] UnsortedLogFilePaths { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * unsortedLogFileNames;
    //  [Abstract]
    //  [Export("unsortedLogFileNames", ArgumentSemantic.Strong)]
    //  string[] UnsortedLogFileNames { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<BDFDDLogFileInfo *> * unsortedLogFileInfos;
    //  [Abstract]
    //  [Export("unsortedLogFileInfos", ArgumentSemantic.Strong)]
    //  BDFDDLogFileInfo[] UnsortedLogFileInfos { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * sortedLogFilePaths;
    //  [Abstract]
    //  [Export("sortedLogFilePaths", ArgumentSemantic.Strong)]
    //  string[] SortedLogFilePaths { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<NSString *> * sortedLogFileNames;
    //  [Abstract]
    //  [Export("sortedLogFileNames", ArgumentSemantic.Strong)]
    //  string[] SortedLogFileNames { get; }

    //  // @required @property (readonly, nonatomic, strong) NSArray<BDFDDLogFileInfo *> * sortedLogFileInfos;
    //  [Abstract]
    //  [Export("sortedLogFileInfos", ArgumentSemantic.Strong)]
    //  BDFDDLogFileInfo[] SortedLogFileInfos { get; }

    //  // @required -(NSString *)createNewLogFile;
    //  [Abstract]
    //  [Export("createNewLogFile")]
    //  [Verify(MethodToProperty)]
    //  string CreateNewLogFile { get; }

    //  // @optional -(void)didArchiveLogFile:(NSString *)logFilePath __attribute__((swift_name("didArchiveLogFile(atPath:)")));
    //  [Export("didArchiveLogFile:")]
    //  void DidArchiveLogFile(string logFilePath);

    //  // @optional -(void)didRollAndArchiveLogFile:(NSString *)logFilePath __attribute__((swift_name("didRollAndArchiveLogFile(atPath:)")));
    //  [Export("didRollAndArchiveLogFile:")]
    //  void DidRollAndArchiveLogFile(string logFilePath);
    //}

    //// @interface BDFDDLogFileManagerDefault : NSObject <BDFDDLogFileManager>
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogFileManagerDefault : IBDFDDLogFileManager
    //{
    //  // -(instancetype)initWithLogsDirectory:(NSString *)logsDirectory __attribute__((objc_designated_initializer));
    //  [Export("initWithLogsDirectory:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(string logsDirectory);

    //  // -(instancetype)initWithLogsDirectory:(NSString *)logsDirectory defaultFileProtectionLevel:(NSFileProtectionType)fileProtectionLevel;
    //  [Export("initWithLogsDirectory:defaultFileProtectionLevel:")]
    //  IntPtr Constructor(string logsDirectory, string fileProtectionLevel);

    //  // @property (readonly, copy) NSString * newLogFileName;
    //  [Export("newLogFileName")]
    //  string NewLogFileName { get; }

    //  // -(BOOL)isLogFile:(NSString *)fileName __attribute__((swift_name("isLogFile(withName:)")));
    //  [Export("isLogFile:")]
    //  bool IsLogFile(string fileName);
    //}

    //// @interface BDFDDLogFileFormatterDefault : NSObject <BDFDDLogFormatter>
    //[BaseType(typeof(NSObject))]
    //interface BDFDDLogFileFormatterDefault : IBDFDDLogFormatter
    //{
    //  // -(instancetype)initWithDateFormatter:(NSDateFormatter *)dateFormatter __attribute__((objc_designated_initializer));
    //  [Export("initWithDateFormatter:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(NSDateFormatter dateFormatter);
    //}

    //// @interface BDFDDFileLogger : BDFDDAbstractLogger <BDFDDLogger>
    //[BaseType(typeof(BDFDDAbstractLogger))]
    //interface BDFDDFileLogger : IBDFDDLogger
    //{
    //  // -(instancetype)initWithLogFileManager:(id<BDFDDLogFileManager>)logFileManager __attribute__((objc_designated_initializer));
    //  [Export("initWithLogFileManager:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(BDFDDLogFileManager logFileManager);

    //  // -(void)willLogMessage __attribute__((objc_requires_super));
    //  [Export("willLogMessage")]
    //  [RequiresSuper]
    //  void WillLogMessage();

    //  // -(void)didLogMessage __attribute__((objc_requires_super));
    //  [Export("didLogMessage")]
    //  [RequiresSuper]
    //  void DidLogMessage();

    //  // -(void)flush __attribute__((objc_requires_super));
    //  [Export("flush")]
    //  [RequiresSuper]
    //  void Flush();

    //  // -(BOOL)shouldArchiveRecentLogFileInfo:(BDFDDLogFileInfo *)recentLogFileInfo;
    //  [Export("shouldArchiveRecentLogFileInfo:")]
    //  bool ShouldArchiveRecentLogFileInfo(BDFDDLogFileInfo recentLogFileInfo);

    //  // @property (assign, readwrite) unsigned long long maximumFileSize;
    //  [Export("maximumFileSize")]
    //  ulong MaximumFileSize { get; set; }

    //  // @property (assign, readwrite) NSTimeInterval rollingFrequency;
    //  [Export("rollingFrequency")]
    //  double RollingFrequency { get; set; }

    //  // @property (assign, readwrite, atomic) BOOL doNotReuseLogFiles;
    //  [Export("doNotReuseLogFiles")]
    //  bool DoNotReuseLogFiles { get; set; }

    //  // @property (readonly, nonatomic, strong) id<BDFDDLogFileManager> logFileManager;
    //  [Export("logFileManager", ArgumentSemantic.Strong)]
    //  BDFDDLogFileManager LogFileManager { get; }

    //  // @property (assign, readwrite, nonatomic) BOOL automaticallyAppendNewlineForCustomFormatters;
    //  [Export("automaticallyAppendNewlineForCustomFormatters")]
    //  bool AutomaticallyAppendNewlineForCustomFormatters { get; set; }

    //  // -(void)rollLogFileWithCompletionBlock:(void (^)(void))completionBlock __attribute__((swift_name("rollLogFile(withCompletion:)")));
    //  [Export("rollLogFileWithCompletionBlock:")]
    //  void RollLogFileWithCompletionBlock(Action completionBlock);

    //  // -(void)rollLogFile __attribute__((deprecated("")));
    //  [Export("rollLogFile")]
    //  void RollLogFile();

    //  // @property (readonly, nonatomic, strong) BDFDDLogFileInfo * currentLogFileInfo;
    //  [Export("currentLogFileInfo", ArgumentSemantic.Strong)]
    //  BDFDDLogFileInfo CurrentLogFileInfo { get; }
    //}

    //// @interface BDFDDLogFileInfo : NSObject
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BDFDDLogFileInfo
    //{
    //  // @property (readonly, nonatomic, strong) NSString * filePath;
    //  [Export("filePath", ArgumentSemantic.Strong)]
    //  string FilePath { get; }

    //  // @property (readonly, nonatomic, strong) NSString * fileName;
    //  [Export("fileName", ArgumentSemantic.Strong)]
    //  string FileName { get; }

    //  // @property (readonly, nonatomic, strong) NSDictionary<NSFileAttributeKey,id> * fileAttributes;
    //  [Export("fileAttributes", ArgumentSemantic.Strong)]
    //  NSDictionary<NSString, NSObject> FileAttributes { get; }

    //  // @property (readonly, nonatomic, strong) NSDate * creationDate;
    //  [Export("creationDate", ArgumentSemantic.Strong)]
    //  NSDate CreationDate { get; }

    //  // @property (readonly, nonatomic, strong) NSDate * modificationDate;
    //  [Export("modificationDate", ArgumentSemantic.Strong)]
    //  NSDate ModificationDate { get; }

    //  // @property (readonly, nonatomic) unsigned long long fileSize;
    //  [Export("fileSize")]
    //  ulong FileSize { get; }

    //  // @property (readonly, nonatomic) NSTimeInterval age;
    //  [Export("age")]
    //  double Age { get; }

    //  // @property (readwrite, nonatomic) BOOL isArchived;
    //  [Export("isArchived")]
    //  bool IsArchived { get; set; }

    //  // +(instancetype)logFileWithPath:(NSString *)filePath __attribute__((availability(swift, unavailable)));
    //  [Unavailable(PlatformName.Swift)]
    //  [Static]
    //  [Export("logFileWithPath:")]
    //  BDFDDLogFileInfo LogFileWithPath(string filePath);

    //  // -(instancetype)initWithFilePath:(NSString *)filePath __attribute__((objc_designated_initializer));
    //  [Export("initWithFilePath:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(string filePath);

    //  // -(void)reset;
    //  [Export("reset")]
    //  void Reset();

    //  // -(void)renameFile:(NSString *)newFileName __attribute__((swift_name("renameFile(to:)")));
    //  [Export("renameFile:")]
    //  void RenameFile(string newFileName);

    //  // -(BOOL)hasExtendedAttributeWithName:(NSString *)attrName;
    //  [Export("hasExtendedAttributeWithName:")]
    //  bool HasExtendedAttributeWithName(string attrName);

    //  // -(void)addExtendedAttributeWithName:(NSString *)attrName;
    //  [Export("addExtendedAttributeWithName:")]
    //  void AddExtendedAttributeWithName(string attrName);

    //  // -(void)removeExtendedAttributeWithName:(NSString *)attrName;
    //  [Export("removeExtendedAttributeWithName:")]
    //  void RemoveExtendedAttributeWithName(string attrName);

    //  // -(NSComparisonResult)reverseCompareByCreationDate:(BDFDDLogFileInfo *)another;
    //  [Export("reverseCompareByCreationDate:")]
    //  NSComparisonResult ReverseCompareByCreationDate(BDFDDLogFileInfo another);

    //  // -(NSComparisonResult)reverseCompareByModificationDate:(BDFDDLogFileInfo *)another;
    //  [Export("reverseCompareByModificationDate:")]
    //  NSComparisonResult ReverseCompareByModificationDate(BDFDDLogFileInfo another);
    //}

    //// @interface BDFDDOSLogger : BDFDDAbstractLogger <BDFDDLogger>
    //[BaseType(typeof(BDFDDAbstractLogger))]
    //interface BDFDDOSLogger : IBDFDDLogger
    //{
    //  // @property (readonly, strong, class) BDFDDOSLogger * sharedInstance;
    //  [Static]
    //  [Export("sharedInstance", ArgumentSemantic.Strong)]
    //  BDFDDOSLogger SharedInstance { get; }
    //}

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

        // @property (nonatomic, class) BDFDDLogLevel logLevel;
        [Static]
        [Export("logLevel", ArgumentSemantic.Assign)]
        BDFDDLogLevel LogLevel { get; set; }

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

    //[Static]
    //[Verify(ConstantsInterfaceAssociation)]
    //partial interface Constants
    //{
    //  // extern double BandyerVersionNumber;
    //  [Field("BandyerVersionNumber", "__Internal")]
    //  double BandyerVersionNumber { get; }

    //  // extern const unsigned char [] BandyerVersionString;
    //  [Field("BandyerVersionString", "__Internal")]
    //  byte[] BandyerVersionString { get; }
    //}

    //// @interface BDKParticipantsFormatter : NSFormatter
    //[BaseType(typeof(NSFormatter))]
    //interface BDKParticipantsFormatter
    //{
    //}

    //// @interface BDFGlobals : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BDFGlobals
    //{
    //  // -(id _Nullable)objectForKeyedSubscript:(NSString * _Nonnull)key;
    //  [Export("objectForKeyedSubscript:")]
    //  [return: NullAllowed]
    //  NSObject ObjectForKeyedSubscript(string key);

    //  // -(void)setObject:(id _Nonnull)obj forKeyedSubscript:(NSString * _Nonnull)key;
    //  [Export("setObject:forKeyedSubscript:")]
    //  void SetObject(NSObject obj, string key);

    //  // +(instancetype _Nonnull)instance;
    //  [Static]
    //  [Export("instance")]
    //  BDFGlobals Instance();
    //}

    //// @interface CommCenterProperties (BDFGlobals)
    //[Category]
    //[BaseType(typeof(BDFGlobals))]
    //interface BDFGlobals_CommCenterProperties
    //{
    //  // @property (copy, nonatomic) NSString * _Nullable bcx_appId __attribute__((swift_name("appId")));
    //  [NullAllowed, Export("bcx_appId")]
    //  string Bcx_appId { get; set; }

    //  // @property (copy, nonatomic) BCXConfig * _Nullable bcx_config __attribute__((swift_name("config")));
    //  [NullAllowed, Export("bcx_config", ArgumentSemantic.Copy)]
    //  BCXConfig Bcx_config { get; set; }

    //  // @property (nonatomic, strong) dispatch_queue_t _Nullable bcx_privateQueue;
    //  [NullAllowed, Export("bcx_privateQueue", ArgumentSemantic.Strong)]
    //  DispatchQueue Bcx_privateQueue { get; set; }

    //  // @property (nonatomic, strong) id<BCXUser> _Nullable bcx_user __attribute__((swift_name("user")));
    //  [NullAllowed, Export("bcx_user", ArgumentSemantic.Strong)]
    //  BCXUser Bcx_user { get; set; }

    //  // @property (nonatomic, class) BDFDDLog * _Nullable bcx_log;
    //  [Static]
    //  [NullAllowed, Export("bcx_log", ArgumentSemantic.Assign)]
    //  BDFDDLog Bcx_log { get; set; }

    //  // @property (nonatomic, class) BDFDDLogLevel bcx_logLevel;
    //  [Static]
    //  [Export("bcx_logLevel", ArgumentSemantic.Assign)]
    //  BDFDDLogLevel Bcx_logLevel { get; set; }

    //  // @property (readonly, nonatomic, class) NSInteger bcx_logContext;
    //  [Static]
    //  [Export("bcx_logContext")]
    //  nint Bcx_logContext { get; }

    //  // @property (readonly, nonatomic, class) NSString * _Nonnull bcx_logTag;
    //  [Static]
    //  [Export("bcx_logTag")]
    //  string Bcx_logTag { get; }

    //  // +(NSString * _Nullable)bcx_appId __attribute__((swift_name("Globals.appId()")));
    //  [Static]
    //  [NullAllowed, Export("bcx_appId")]
    //  [Verify(MethodToProperty)]
    //  string Bcx_appId { get; }

    //  // +(BCXConfig * _Nullable)bcx_config;
    //  [Static]
    //  [NullAllowed, Export("bcx_config")]
    //  [Verify(MethodToProperty)]
    //  BCXConfig Bcx_config { get; }

    //  // +(dispatch_queue_t _Nonnull)bcx_privateQueue;
    //  [Static]
    //  [Export("bcx_privateQueue")]
    //  [Verify(MethodToProperty)]
    //  DispatchQueue Bcx_privateQueue { get; }

    //  // +(id<BCXUser> _Nullable)bcx_user __attribute__((swift_name("Globals.user()")));
    //  [Static]
    //  [NullAllowed, Export("bcx_user")]
    //  [Verify(MethodToProperty)]
    //  BCXUser Bcx_user { get; }
    //}

    //// @interface Internal (BDKEnvironment)
    //[Category]
    //[BaseType(typeof(BDKEnvironment))]
    //interface BDKEnvironment_Internal
    //{
    //  // @property (readonly, nonatomic, strong) id<BDKEnvironmentProperties> _Nonnull properties;
    //  [Export("properties", ArgumentSemantic.Strong)]
    //  BDKEnvironmentProperties Properties { get; }
    //}

    //// @protocol BCXEnvironmentProperties <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BCXEnvironmentProperties
    //{
    //  // @required @property (readonly, nonatomic) NSString * _Nonnull name;
    //  [Abstract]
    //  [Export("name")]
    //  string Name { get; }

    //  // @required @property (readonly, nonatomic) NSURL * _Nonnull commCenterURL;
    //  [Abstract]
    //  [Export("commCenterURL")]
    //  NSUrl CommCenterURL { get; }

    //  // @required @property (readonly, nonatomic) NSString * _Nullable commCenterPath;
    //  [Abstract]
    //  [NullAllowed, Export("commCenterPath")]
    //  string CommCenterPath { get; }
    //}

    //// @protocol BDKEnvironmentProperties <BCXEnvironmentProperties>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //interface BDKEnvironmentProperties : IBCXEnvironmentProperties
    //{
    //  // @required @property (readonly, nonatomic) NSString * _Nonnull name;
    //  [Abstract]
    //  [Export("name")]
    //  string Name { get; }

    //  // @required @property (readonly, nonatomic) NSURL * _Nonnull baseURL;
    //  [Abstract]
    //  [Export("baseURL")]
    //  NSUrl BaseURL { get; }

    //  // @required @property (readonly, nonatomic) NSURL * _Nonnull whiteboardURL;
    //  [Abstract]
    //  [Export("whiteboardURL")]
    //  NSUrl WhiteboardURL { get; }
    //}

    //// @interface BandyerCommunicationCenter : NSObject
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BandyerCommunicationCenter
    //{
    //  // @property (readonly, copy, nonatomic) BCXConfig * _Nonnull config;
    //  [Export("config", ArgumentSemantic.Copy)]
    //  BCXConfig Config { get; }

    //  // @property (readonly, nonatomic, strong) id<BCXCallRegistry> _Nonnull callRegistry;
    //  [Export("callRegistry", ArgumentSemantic.Strong)]
    //  BCXCallRegistry CallRegistry { get; }

    //  // @property (readonly, nonatomic, strong) id<BCXCallClient> _Nonnull callClient;
    //  [Export("callClient", ArgumentSemantic.Strong)]
    //  BCXCallClient CallClient { get; }

    //  // -(void)initializeWithApplicationId:(NSString * _Nonnull)appId;
    //  [Export("initializeWithApplicationId:")]
    //  void InitializeWithApplicationId(string appId);

    //  // -(void)initializeWithApplicationId:(NSString * _Nonnull)appId config:(BCXConfig * _Nonnull)config;
    //  [Export("initializeWithApplicationId:config:")]
    //  void InitializeWithApplicationId(string appId, BCXConfig config);

    //  // +(instancetype _Nonnull)instance;
    //  [Static]
    //  [Export("instance")]
    //  BandyerCommunicationCenter Instance();
    //}

    //// @interface Internal (BCHChatNotification)
    //[Category]
    //[BaseType(typeof(BCHChatNotification))]
    //interface BCHChatNotification_Internal
    //{
    //  // @property (readonly, copy, nonatomic) NSString * _Nonnull channelId;
    //  [Export("channelId")]
    //  string ChannelId { get; }

    //  // -(instancetype _Nonnull)initWithChannelId:(NSString * _Nonnull)channelId;
    //  [Export("initWithChannelId:")]
    //  IntPtr Constructor(string channelId);
    //}

    //// @interface BDKDefaultFetcher : NSObject <BDKUserInfoFetcher>
    //[BaseType(typeof(NSObject))]
    //interface BDKDefaultFetcher : IBDKUserInfoFetcher
    //{
    //  // +(instancetype _Nonnull)instance;
    //  [Static]
    //  [Export("instance")]
    //  BDKDefaultFetcher Instance();
    //}

    //// @interface BCXPlatform : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BCXPlatform
    //{
    //  // @property (readonly, nonatomic, strong) NSString * _Nonnull type;
    //  [Export("type", ArgumentSemantic.Strong)]
    //  string Type { get; }

    //  // @property (readonly, nonatomic, strong) NSString * _Nonnull agent;
    //  [Export("agent", ArgumentSemantic.Strong)]
    //  string Agent { get; }

    //  // @property (readonly, nonatomic, strong) NSString * _Nonnull clientName;
    //  [Export("clientName", ArgumentSemantic.Strong)]
    //  string ClientName { get; }

    //  // @property (readonly, nonatomic, strong) NSString * _Nonnull clientVersion;
    //  [Export("clientVersion", ArgumentSemantic.Strong)]
    //  string ClientVersion { get; }

    //  // +(instancetype _Nonnull)currentPlatform;
    //  [Static]
    //  [Export("currentPlatform")]
    //  BCXPlatform CurrentPlatform();
    //}

    //// @protocol BAVVideoViewDelegate <NSObject>
    //[Protocol, Model(AutoGeneratedName = true)]
    //[BaseType(typeof(NSObject))]
    //interface BAVVideoViewDelegate
    //{
    //  // @optional -(void)videoView:(BAVVideoView * _Nonnull)view didChangeVideoSizeFittingMode:(BAVVideoSizeFittingMode)mode;
    //  [Export("videoView:didChangeVideoSizeFittingMode:")]
    //  void DidChangeVideoSizeFittingMode(BAVVideoView view, BAVVideoSizeFittingMode mode);

    //  // @optional -(void)videoView:(BAVVideoView * _Nonnull)view didChangeVideoSize:(CGSize)size;
    //  [Export("videoView:didChangeVideoSize:")]
    //  void DidChangeVideoSize(BAVVideoView view, CGSize size);
    //}

    //// @interface BAVVideoView : UIView
    //[BaseType(typeof(UIView))]
    //interface BAVVideoView
    //{
    //  [Wrap("WeakDelegate")]
    //  [NullAllowed]
    //  BAVVideoViewDelegate Delegate { get; set; }

    //  // @property (nonatomic, weak) id<BAVVideoViewDelegate> _Nullable delegate __attribute__((iboutlet));
    //  [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
    //  NSObject WeakDelegate { get; set; }

    //  // @property (readonly, assign, nonatomic) CGSize originalVideoSize;
    //  [Export("originalVideoSize", ArgumentSemantic.Assign)]
    //  CGSize OriginalVideoSize { get; }

    //  // @property (readonly, assign, nonatomic) CGSize videoSize;
    //  [Export("videoSize", ArgumentSemantic.Assign)]
    //  CGSize VideoSize { get; }

    //  // @property (nonatomic, strong) BAVStream * _Nullable stream;
    //  [NullAllowed, Export("stream", ArgumentSemantic.Strong)]
    //  BAVStream Stream { get; set; }

    //  // @property (assign, nonatomic) BOOL useScreenScaleFactor;
    //  [Export("useScreenScaleFactor")]
    //  bool UseScreenScaleFactor { get; set; }

    //  // @property (assign, nonatomic) BAVVideoSizeFittingMode videoSizeFittingMode;
    //  [Export("videoSizeFittingMode", ArgumentSemantic.Assign)]
    //  BAVVideoSizeFittingMode VideoSizeFittingMode { get; set; }

    //  // -(void)startRendering;
    //  [Export("startRendering")]
    //  void StartRendering();

    //  // -(void)stopRendering;
    //  [Export("stopRendering")]
    //  void StopRendering();

    //  // -(void)detachStream;
    //  [Export("detachStream")]
    //  void DetachStream();
    //}

    //// @interface BAVZoomableVideoView : UIView
    //[BaseType(typeof(UIView))]
    //interface BAVZoomableVideoView
    //{
    //  // @property (readonly, assign, nonatomic) CGSize originalVideoSize;
    //  [Export("originalVideoSize", ArgumentSemantic.Assign)]
    //  CGSize OriginalVideoSize { get; }

    //  // @property (readonly, assign, nonatomic) CGSize videoSize;
    //  [Export("videoSize", ArgumentSemantic.Assign)]
    //  CGSize VideoSize { get; }

    //  // @property (nonatomic, strong) BAVStream * _Nullable stream;
    //  [NullAllowed, Export("stream", ArgumentSemantic.Strong)]
    //  BAVStream Stream { get; set; }

    //  // -(void)startRendering;
    //  [Export("startRendering")]
    //  void StartRendering();

    //  // -(void)stopRendering;
    //  [Export("stopRendering")]
    //  void StopRendering();

    //  // -(void)detachStream;
    //  [Export("detachStream")]
    //  void DetachStream();
    //}

    //// @interface BUIConstraintsToSuperview (UIView)
    //[Category]
    //[BaseType(typeof(UIView))]
    //interface UIView_BUIConstraintsToSuperview
    //{
    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToSuperview __attribute__((swift_name("constrainToSuperview()")));
    //  [Export("bui_constrainToSuperview")]
    //  [Verify(MethodToProperty)]
    //  NSLayoutConstraint[] Bui_constrainToSuperview { get; }

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToSuperview:(UIEdgeInsets)insets __attribute__((swift_name("constrainToSuperview(insets:)")));
    //  [Export("bui_constrainToSuperview:")]
    //  NSLayoutConstraint[] Bui_constrainToSuperview(UIEdgeInsets insets);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToSuperview:(UIEdgeInsets)insets activate:(BOOL)activate __attribute__((swift_name("constrainToSuperview(insets:activate:)")));
    //  [Export("bui_constrainToSuperview:activate:")]
    //  NSLayoutConstraint[] Bui_constrainToSuperview(UIEdgeInsets insets, bool activate);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToSuperviewWithPadding:(CGFloat)padding __attribute__((swift_name("constrainToSuperview(padding:)")));
    //  [Export("bui_constrainToSuperviewWithPadding:")]
    //  NSLayoutConstraint[] Bui_constrainToSuperviewWithPadding(nfloat padding);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToSuperviewWithPadding:(CGFloat)padding activate:(BOOL)activate __attribute__((swift_name("constrainToSuperview(padding:activate:)")));
    //  [Export("bui_constrainToSuperviewWithPadding:activate:")]
    //  NSLayoutConstraint[] Bui_constrainToSuperviewWithPadding(nfloat padding, bool activate);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToViewEdges:(UIView * _Nonnull)view __attribute__((swift_name("constrainToEdgesOf(view:)")));
    //  [Export("bui_constrainToViewEdges:")]
    //  NSLayoutConstraint[] Bui_constrainToViewEdges(UIView view);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToViewEdges:(UIView * _Nonnull)view insets:(UIEdgeInsets)insets __attribute__((swift_name("constrainToEdgesOf(view:insets:)")));
    //  [Export("bui_constrainToViewEdges:insets:")]
    //  NSLayoutConstraint[] Bui_constrainToViewEdges(UIView view, UIEdgeInsets insets);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToViewEdges:(UIView * _Nonnull)view insets:(UIEdgeInsets)insets activate:(BOOL)activate __attribute__((swift_name("constrainToEdgesOf(view:insets:activate:)")));
    //  [Export("bui_constrainToViewEdges:insets:activate:")]
    //  NSLayoutConstraint[] Bui_constrainToViewEdges(UIView view, UIEdgeInsets insets, bool activate);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToViewEdges:(UIView * _Nonnull)view padding:(CGFloat)padding __attribute__((swift_name("constrainToEdgesOf(view:padding:)")));
    //  [Export("bui_constrainToViewEdges:padding:")]
    //  NSLayoutConstraint[] Bui_constrainToViewEdges(UIView view, nfloat padding);

    //  // -(NSArray<NSLayoutConstraint *> * _Nonnull)bui_constrainToViewEdges:(UIView * _Nonnull)view padding:(CGFloat)padding activate:(BOOL)activate __attribute__((swift_name("constrainToEdgesOf(view:padding:activate:)")));
    //  [Export("bui_constrainToViewEdges:padding:activate:")]
    //  NSLayoutConstraint[] Bui_constrainToViewEdges(UIView view, nfloat padding, bool activate);
    //}

    //// @protocol BAVSDPFilter <NSObject>
    ///*
    //  Check whether adding [Model] to this declaration is appropriate.
    //  [Model] is used to generate a C# class that implements this protocol,
    //  and might be useful for protocols that consumers are supposed to implement,
    //  since consumers can subclass the generated class instead of implementing
    //  the generated interface. If consumers are not supposed to implement this
    //  protocol, then [Model] is redundant and will generate code that will never
    //  be used.
    //*/
    //[Protocol]
    //[BaseType(typeof(NSObject))]
    //interface BAVSDPFilter
    //{
    //  // @required -(RTCSessionDescription * _Nonnull)apply:(RTCSessionDescription * _Nonnull)sdp;
    //  [Abstract]
    //  [Export("apply:")]
    //  RTCSessionDescription Apply(RTCSessionDescription sdp);
    //}

    //// @interface BDKCallBannerController : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BDKCallBannerController
    //{
    //  [Wrap("WeakDelegate")]
    //  [NullAllowed]
    //  BDKCallBannerControllerDelegate Delegate { get; set; }

    //  // @property (nonatomic, weak) id<BDKCallBannerControllerDelegate> _Nullable delegate;
    //  [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
    //  NSObject WeakDelegate { get; set; }

    //  // @property (nonatomic, weak) UIViewController * _Nullable parentViewController;
    //  [NullAllowed, Export("parentViewController", ArgumentSemantic.Weak)]
    //  UIViewController ParentViewController { get; set; }

    //  // -(void)show;
    //  [Export("show")]
    //  void Show();

    //  // -(void)hide;
    //  [Export("hide")]
    //  void Hide();

    //  // -(void)viewWillTransitionTo:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
    //  [Export("viewWillTransitionTo:withTransitionCoordinator:")]
    //  void ViewWillTransitionTo(CGSize size, UIViewControllerTransitionCoordinator coordinator);
    //}

    //// @protocol BDKCallBannerControllerDelegate
    //[Protocol, Model(AutoGeneratedName = true)]
    //interface BDKCallBannerControllerDelegate
    //{
    //  // @required -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller didTouch:(BDKCallBannerView * _Nonnull)banner;
    //  [Abstract]
    //  [Export("callBannerController:didTouch:")]
    //  void DidTouch(BDKCallBannerController controller, BDKCallBannerView banner);

    //  // @optional -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willShow:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("callBannerController:willShow:")]
    //  void WillShow(BDKCallBannerController controller, BDKCallBannerView banner);

    //  // @optional -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willHide:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("callBannerController:willHide:")]
    //  void WillHide(BDKCallBannerController controller, BDKCallBannerView banner);
    //}

    //// @interface BDKCallBannerView : UIView
    //[BaseType(typeof(UIView))]
    //interface BDKCallBannerView
    //{
    //}

    // @interface BDKCallWindow : UIWindow
    [BaseType(typeof(UIWindow))]
    interface BDKCallWindow
    {
      // @property (readonly, nonatomic, strong, class) BDKCallWindow * _Nullable instance;
      [Static]
      [NullAllowed, Export("instance", ArgumentSemantic.Strong)]
      BDKCallWindow Instance { get; }

      //[Wrap("WeakCallDelegate")]
      //[NullAllowed]
      //BDKCallWindowDelegate CallDelegate { get; set; }

      // @property (nonatomic, weak) id<BDKCallWindowDelegate> _Nullable callDelegate;
      [NullAllowed, Export("callDelegate", ArgumentSemantic.Weak)]
      NSObject WeakCallDelegate { get; set; }

      // @property (readonly, nonatomic, strong) id<BDKIntent> _Nullable intent;
      [NullAllowed, Export("intent", ArgumentSemantic.Strong)]
      BDKIntent Intent { get; }

      // -(void)shouldPresentCallViewControllerWithIntent:(id<BDKIntent> _Nullable)intent completion:(void (^ _Nonnull)(BOOL))completion;
      [Export("shouldPresentCallViewControllerWithIntent:completion:")]
      void ShouldPresentCallViewControllerWithIntent([NullAllowed] IBDKIntent intent, Action<bool> completion);

      // -(void)dismissCallViewControllerWithCompletion:(void (^ _Nonnull)(void))completion;
      [Export("dismissCallViewControllerWithCompletion:")]
      void DismissCallViewControllerWithCompletion(Action completion);

      // -(void)setConfiguration:(BDKCallViewControllerConfiguration * _Nullable)configuration;
      [Export("setConfiguration:")]
      void SetConfiguration([NullAllowed] BDKCallViewControllerConfiguration configuration);

      // -(void)handleINStartVideoCallIntent:(INStartVideoCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=10.0, deprecated=13.0)));
      [Introduced(PlatformName.iOS, 10, 0, message: "handle(startVideoCallIntent:) is deprecated. Please use handle(startCallIntent:) instead")]
      [Deprecated(PlatformName.iOS, 13, 0, message: "handle(startVideoCallIntent:) is deprecated. Please use handle(startCallIntent:) instead")]
      [Export("handleINStartVideoCallIntent:")]
      void HandleINStartVideoCallIntent(INStartVideoCallIntent intent);

      // -(void)handleINStartCallIntent:(INStartCallIntent * _Nonnull)intent __attribute__((availability(ios, introduced=13.0)));
      [iOS(13, 0)]
      [Export("handleINStartCallIntent:")]
      void HandleINStartCallIntent(INStartCallIntent intent);
    }

    //// @protocol BCHMessageNotificationControllerDelegate
    //[Protocol, Model(AutoGeneratedName = true)]
    //interface BCHMessageNotificationControllerDelegate
    //{
    //  // @required -(void)messageNotificationController:(BCHMessageNotificationController * _Nonnull)controller didTouch:(BCHChatNotification * _Nonnull)notification;
    //  [Abstract]
    //  [Export("messageNotificationController:didTouch:")]
    //  void DidTouch(BCHMessageNotificationController controller, BCHChatNotification notification);
    //}

    //// @interface Bandyer_Swift_1640 (BDKCallWindow) <BCHMessageNotificationControllerDelegate>
    //[Category]
    //[BaseType(typeof(BDKCallWindow))]
    //interface BDKCallWindow_Bandyer_Swift_1640 : IBCHMessageNotificationControllerDelegate
    //{
    //  // -(void)messageNotificationController:(BCHMessageNotificationController * _Nonnull)controller didTouch:(BCHChatNotification * _Nonnull)notification;
    //  [Export("messageNotificationController:didTouch:")]
    //  void MessageNotificationController(BCHMessageNotificationController controller, BCHChatNotification notification);
    //}

    //// @interface Bandyer_Swift_1647 (BDKCallWindow) <BDKCallViewControllerDelegate>
    //[Category]
    //[BaseType(typeof(BDKCallWindow))]
    //interface BDKCallWindow_Bandyer_Swift_1647 : IBDKCallViewControllerDelegate
    //{
    //  // -(void)callViewController:(BDKCallViewController * _Nonnull)controller openChatWith:(NSString * _Nonnull)participantId;
    //  [Export("callViewController:openChatWith:")]
    //  void CallViewController(BDKCallViewController controller, string participantId);

    //  // -(void)callViewControllerDidPressBack:(BDKCallViewController * _Nonnull)controller;
    //  [Export("callViewControllerDidPressBack:")]
    //  void CallViewControllerDidPressBack(BDKCallViewController controller);

    //  // -(void)callViewControllerDidFinish:(BDKCallViewController * _Nonnull)controller;
    //  [Export("callViewControllerDidFinish:")]
    //  void CallViewControllerDidFinish(BDKCallViewController controller);
    //}

    //// @protocol BDKCallWindowDelegate
    //[Protocol, Model(AutoGeneratedName = true)]
    //interface BDKCallWindowDelegate
    //{
    //  // @required -(void)callWindowDidFinish:(BDKCallWindow * _Nonnull)window;
    //  [Abstract]
    //  [Export("callWindowDidFinish:")]
    //  void CallWindowDidFinish(BDKCallWindow window);

    //  // @optional -(void)callWindow:(BDKCallWindow * _Nonnull)window openChatWith:(BCHOpenChatIntent * _Nonnull)intent;
    //  [Export("callWindow:openChatWith:")]
    //  void CallWindow(BDKCallWindow window, BCHOpenChatIntent intent);
    //}

    //// @interface BCHChannelViewController : UIViewController
    //[BaseType(typeof(UIViewController))]
    //interface BCHChannelViewController
    //{
    //  // @property (nonatomic, strong) BCHChannelViewControllerConfiguration * _Nullable configuration;
    //  [NullAllowed, Export("configuration", ArgumentSemantic.Strong)]
    //  BCHChannelViewControllerConfiguration Configuration { get; set; }

    //  [Wrap("WeakDelegate")]
    //  [NullAllowed]
    //  BCHChannelViewControllerDelegate Delegate { get; set; }

    //  // @property (nonatomic, weak) id<BCHChannelViewControllerDelegate> _Nullable delegate;
    //  [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
    //  NSObject WeakDelegate { get; set; }

    //  // @property (nonatomic, strong) BCHOpenChatIntent * _Nullable intent;
    //  [NullAllowed, Export("intent", ArgumentSemantic.Strong)]
    //  BCHOpenChatIntent Intent { get; set; }

    //  // -(instancetype _Nonnull)initWithNibName:(NSString * _Nullable)nibNameOrNil bundle:(NSBundle * _Nullable)nibBundleOrNil __attribute__((objc_designated_initializer));
    //  [Export("initWithNibName:bundle:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor([NullAllowed] string nibNameOrNil, [NullAllowed] NSBundle nibBundleOrNil);

    //  // -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)coder __attribute__((objc_designated_initializer));
    //  [Export("initWithCoder:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(NSCoder coder);

    //  // -(void)viewDidLoad;
    //  [Export("viewDidLoad")]
    //  void ViewDidLoad();

    //  // -(void)viewWillLayoutSubviews;
    //  [Export("viewWillLayoutSubviews")]
    //  void ViewWillLayoutSubviews();

    //  // -(void)viewWillAppear:(BOOL)animated;
    //  [Export("viewWillAppear:")]
    //  void ViewWillAppear(bool animated);

    //  // -(void)viewWillDisappear:(BOOL)animated;
    //  [Export("viewWillDisappear:")]
    //  void ViewWillDisappear(bool animated);

    //  // -(void)viewDidAppear:(BOOL)animated;
    //  [Export("viewDidAppear:")]
    //  void ViewDidAppear(bool animated);

    //  // -(void)viewDidDisappear:(BOOL)animated;
    //  [Export("viewDidDisappear:")]
    //  void ViewDidDisappear(bool animated);

    //  // -(void)viewWillTransitionToSize:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
    //  [Export("viewWillTransitionToSize:withTransitionCoordinator:")]
    //  void ViewWillTransitionToSize(CGSize size, UIViewControllerTransitionCoordinator coordinator);

    //  // @property (readonly, nonatomic) BOOL canBecomeFirstResponder;
    //  [Export("canBecomeFirstResponder")]
    //  bool CanBecomeFirstResponder { get; }

    //  // @property (readonly, nonatomic, strong) UIView * _Nullable inputAccessoryView;
    //  [NullAllowed, Export("inputAccessoryView", ArgumentSemantic.Strong)]
    //  UIView InputAccessoryView { get; }
    //}

    //// @interface Bandyer_Swift_1720 (BCHChannelViewController) <UIAdaptivePresentationControllerDelegate>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1720 : IUIAdaptivePresentationControllerDelegate
    //{
    //  // -(void)presentationControllerDidDismiss:(UIPresentationController * _Nonnull)presentationController;
    //  [Export("presentationControllerDidDismiss:")]
    //  void PresentationControllerDidDismiss(UIPresentationController presentationController);
    //}

    //// @interface Bandyer_Swift_1726 (BCHChannelViewController) <BCHMessageNotificationControllerDelegate>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1726 : IBCHMessageNotificationControllerDelegate
    //{
    //  // -(void)messageNotificationController:(BCHMessageNotificationController * _Nonnull)controller didTouch:(BCHChatNotification * _Nonnull)notification;
    //  [Export("messageNotificationController:didTouch:")]
    //  void MessageNotificationController(BCHMessageNotificationController controller, BCHChatNotification notification);
    //}

    //// @interface Bandyer_Swift_1733 (BCHChannelViewController) <UINavigationBarDelegate>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1733 : IUINavigationBarDelegate
    //{
    //  // -(UIBarPosition)positionForBar:(id<UIBarPositioning> _Nonnull)bar __attribute__((warn_unused_result));
    //  [Export("positionForBar:")]
    //  UIBarPosition PositionForBar(UIBarPositioning bar);
    //}

    //// @interface Bandyer_Swift_1741 (BCHChannelViewController) <BCXCallRegistryObserver>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1741 : IBCXCallRegistryObserver
    //{
    //  // -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didAddCall:(id<BCXCall> _Nonnull)call;
    //  [Export("registry:didAddCall:")]
    //  void Registry(BCXCallRegistry registry, BCXCall call);

    //  // -(void)registry:(id<BCXCallRegistry> _Nonnull)registry didRemoveCall:(id<BCXCall> _Nonnull)call;
    //  [Export("registry:didRemoveCall:")]
    //  void Registry(BCXCallRegistry registry, BCXCall call);
    //}

    //// @interface Bandyer_Swift_1755 (BCHChannelViewController) <BDKCallBannerControllerDelegate>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1755 : IBDKCallBannerControllerDelegate
    //{
    //  // -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller didTouch:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("callBannerController:didTouch:")]
    //  void CallBannerController(BDKCallBannerController controller, BDKCallBannerView banner);

    //  // -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willShow:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("callBannerController:willShow:")]
    //  void CallBannerController(BDKCallBannerController controller, BDKCallBannerView banner);

    //  // -(void)callBannerController:(BDKCallBannerController * _Nonnull)controller willHide:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("callBannerController:willHide:")]
    //  void CallBannerController(BDKCallBannerController controller, BDKCallBannerView banner);
    //}

    //// @interface Bandyer_Swift_1766 (BCHChannelViewController) <BCXCallClientObserver>
    //[Category]
    //[BaseType(typeof(BCHChannelViewController))]
    //interface BCHChannelViewController_Bandyer_Swift_1766 : IBCXCallClientObserver
    //{
    //  // -(void)callClientDidStart:(id<BCXCallClient> _Nonnull)client;
    //  [Export("callClientDidStart:")]
    //  void CallClientDidStart(BCXCallClient client);

    //  // -(void)callClientDidPause:(id<BCXCallClient> _Nonnull)client;
    //  [Export("callClientDidPause:")]
    //  void CallClientDidPause(BCXCallClient client);

    //  // -(void)callClientDidResume:(id<BCXCallClient> _Nonnull)client;
    //  [Export("callClientDidResume:")]
    //  void CallClientDidResume(BCXCallClient client);

    //  // -(void)callClientDidStop:(id<BCXCallClient> _Nonnull)client;
    //  [Export("callClientDidStop:")]
    //  void CallClientDidStop(BCXCallClient client);

    //  // -(void)callClient:(id<BCXCallClient> _Nonnull)client didFailWithError:(NSError * _Nonnull)didFailWithError;
    //  [Export("callClient:didFailWithError:")]
    //  void CallClient(BCXCallClient client, NSError didFailWithError);
    //}

    //// @interface BCHChannelViewControllerConfiguration : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BCHChannelViewControllerConfiguration
    //{
    //  // -(instancetype _Nonnull)initWithUserInfoFetcher:(id<BDKUserInfoFetcher> _Nonnull)userInfoFetcher;
    //  [Export("initWithUserInfoFetcher:")]
    //  IntPtr Constructor(BDKUserInfoFetcher userInfoFetcher);

    //  // -(instancetype _Nonnull)initWithAudioButton:(BOOL)audioButton videoButton:(BOOL)videoButton userInfoFetcher:(id<BDKUserInfoFetcher> _Nullable)userInfoFetcher __attribute__((objc_designated_initializer));
    //  [Export("initWithAudioButton:videoButton:userInfoFetcher:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor(bool audioButton, bool videoButton, [NullAllowed] BDKUserInfoFetcher userInfoFetcher);
    //}

    //// @protocol BCHChannelViewControllerDelegate
    //[Protocol, Model(AutoGeneratedName = true)]
    //interface BCHChannelViewControllerDelegate
    //{
    //  // @required -(void)channelViewControllerDidFinish:(BCHChannelViewController * _Nonnull)controller;
    //  [Abstract]
    //  [Export("channelViewControllerDidFinish:")]
    //  void ChannelViewControllerDidFinish(BCHChannelViewController controller);

    //  // @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTouchNotification:(BCHChatNotification * _Nonnull)notification;
    //  [Export("channelViewController:didTouchNotification:")]
    //  void ChannelViewController(BCHChannelViewController controller, BCHChatNotification notification);

    //  // @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTouchBanner:(BDKCallBannerView * _Nonnull)banner;
    //  [Abstract]
    //  [Export("channelViewController:didTouchBanner:")]
    //  void ChannelViewController(BCHChannelViewController controller, BDKCallBannerView banner);

    //  // @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller willHide:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("channelViewController:willHide:")]
    //  void ChannelViewController(BCHChannelViewController controller, BDKCallBannerView banner);

    //  // @optional -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller willShow:(BDKCallBannerView * _Nonnull)banner;
    //  [Export("channelViewController:willShow:")]
    //  void ChannelViewController(BCHChannelViewController controller, BDKCallBannerView banner);

    //  // @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTapAudioCallWith:(NSArray<NSString *> * _Nonnull)users;
    //  [Abstract]
    //  [Export("channelViewController:didTapAudioCallWith:")]
    //  void ChannelViewController(BCHChannelViewController controller, string[] users);

    //  // @required -(void)channelViewController:(BCHChannelViewController * _Nonnull)controller didTapVideoCallWith:(NSArray<NSString *> * _Nonnull)users;
    //  [Abstract]
    //  [Export("channelViewController:didTapVideoCallWith:")]
    //  void ChannelViewController(BCHChannelViewController controller, string[] users);
    //}

    //// @interface BCHMessageNotificationController : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BCHMessageNotificationController
    //{
    //  [Wrap("WeakDelegate")]
    //  [NullAllowed]
    //  BCHMessageNotificationControllerDelegate Delegate { get; set; }

    //  // @property (nonatomic, weak) id<BCHMessageNotificationControllerDelegate> _Nullable delegate;
    //  [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
    //  NSObject WeakDelegate { get; set; }

    //  // @property (nonatomic, weak) UIViewController * _Nullable parentViewController;
    //  [NullAllowed, Export("parentViewController", ArgumentSemantic.Weak)]
    //  UIViewController ParentViewController { get; set; }

    //  // @property (nonatomic, strong) BCHMessageNotificationControllerConfiguration * _Nullable configuration;
    //  [NullAllowed, Export("configuration", ArgumentSemantic.Strong)]
    //  BCHMessageNotificationControllerConfiguration Configuration { get; set; }

    //  // -(void)show;
    //  [Export("show")]
    //  void Show();

    //  // -(void)hide;
    //  [Export("hide")]
    //  void Hide();

    //  // -(void)viewWillTransitionTo:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
    //  [Export("viewWillTransitionTo:withTransitionCoordinator:")]
    //  void ViewWillTransitionTo(CGSize size, UIViewControllerTransitionCoordinator coordinator);
    //}

    //// @interface BCHMessageNotificationControllerConfiguration : NSObject
    //[BaseType(typeof(NSObject))]
    //interface BCHMessageNotificationControllerConfiguration
    //{
    //  // -(instancetype _Nonnull)initWithUserInfoFetcher:(id<BDKUserInfoFetcher> _Nullable)userInfoFetcher __attribute__((objc_designated_initializer));
    //  [Export("initWithUserInfoFetcher:")]
    //  [DesignatedInitializer]
    //  IntPtr Constructor([NullAllowed] BDKUserInfoFetcher userInfoFetcher);
    //}

    //// @interface BCHOpenChatIntent : NSObject <BDKIntent>
    //[BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    //interface BCHOpenChatIntent : IBDKIntent
    //{
    //  // @property (readonly, copy, nonatomic) NSUUID * _Nonnull UUID;
    //  [Export("UUID", ArgumentSemantic.Copy)]
    //  NSUuid UUID { get; }

    //  // +(BCHOpenChatIntent * _Nonnull)openChatWith:(NSString * _Nonnull)participant __attribute__((warn_unused_result));
    //  [Static]
    //  [Export("openChatWith:")]
    //  BCHOpenChatIntent OpenChatWith(string participant);

    //  // +(BCHOpenChatIntent * _Nullable)openChatFrom:(BCHChatNotification * _Nonnull)notification __attribute__((warn_unused_result));
    //  [Static]
    //  [Export("openChatFrom:")]
    //  [return: NullAllowed]
    //  BCHOpenChatIntent OpenChatFrom(BCHChatNotification notification);
    //}
}
