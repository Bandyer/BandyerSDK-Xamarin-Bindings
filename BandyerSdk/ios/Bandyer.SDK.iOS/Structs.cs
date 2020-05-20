// Copyright © 2020 Bandyer. All rights reserved.
// See LICENSE for licensing information

using System;
using CoreFoundation;
using Foundation;

namespace Bandyer
{
    public enum BDKCallType : byte
    {
        CallTypeAudioVideo = 0,
        CallTypeAudioUpgradable,
        CallTypeAudioOnly,
        AudioVideoCallType = CallTypeAudioVideo,
        AudioUpgradableCallType = CallTypeAudioUpgradable,
        AudioOnlyCallType = CallTypeAudioOnly
    }

	//[Native]
	public enum BCXCallClientState : long
	{
		Stopped = 0,
		Starting,
		Running,
		Resuming,
		Paused,
		Reconnecting
	}

	//static class CFunctions
	//{
	//  // extern NSString * _Nonnull NSStringFromBCXCallClientState (BCXCallClientState state);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallClientState(BCXCallClientState state);

	//  // extern NSString * _Nonnull NSStringFromBCXCallDirection (BCXCallDirection direction);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallDirection(BCXCallDirection direction);

	//  // extern NSString * _Nonnull NSStringFromBCXCallState (BCXCallState state);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallState(BCXCallState state);

	//  // extern NSString * _Nonnull NSStringFromBCXCallEndReason (BCXCallEndReason endReason);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallEndReason(BCXCallEndReason endReason);

	//  // extern NSString * _Nonnull NSStringFromBCXDeclineReason (BCXDeclineReason reason);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXDeclineReason(BCXDeclineReason reason);

	//  // extern NSString * _Nonnull NSStringFromBCXCallType (BCXCallType type);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallType(BCXCallType type);

	//  // extern NSString * _Nonnull NSStringFromBCXUserStatus (BCXUserStatus status);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXUserStatus(BCXUserStatus status);

	//  // extern NSString * _Nonnull NSStringFromBCXCallParticipantState (BCXCallParticipantState state);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCXCallParticipantState(BCXCallParticipantState state);

	//  // extern NSString * _Nonnull NSStringFromBCHChatClientState (BCHChatClientState state);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBCHChatClientState(BCHChatClientState state);

	//  // extern NSString * _Nullable BDFDDExtractFileNameWithoutExtension (const char * _Nonnull filePath, BOOL copy);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  [return: NullAllowed]
	//  static extern unsafe NSString BDFDDExtractFileNameWithoutExtension(sbyte* filePath, bool copy);

	//  // BDFDDColor * BDFDDMakeColor (CGFloat r, CGFloat g, CGFloat b);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern UIColor BDFDDMakeColor(nfloat r, nfloat g, nfloat b);

	//  // extern NSString * _Nonnull NSStringFromBAVVideoSizeFittingMode (BAVVideoSizeFittingMode value);
	//  [DllImport("__Internal")]
	//  [Verify(PlatformInvoke)]
	//  static extern NSString NSStringFromBAVVideoSizeFittingMode(BAVVideoSizeFittingMode value);
	//}

	//[Native]
	public enum BCXCallDirection : long
	{
		Incoming = 0,
		Outgoing
	}

	//[Native]
	public enum BCXCallState : long
	{
		Idle,
		Dialing,
		Ringing,
		Answering,
		Declining,
		HangingUp,
		Connecting,
		Connected,
		Ended,
		Failed
	}

	//[Native]
	public enum BCXCallEndReason : long
	{
		Unknown = -1,
		None,
		HangUp,
		Declined,
		AnsweredOnAnotherDevice,
		UserDisconnected,
		TimedOut,
		Error
	}

	//[Native]
	public enum BCXDeclineReason : long
	{
		Unknown = -1,
		None,
		DoNotDisturb,
		NoAnswer,
		Error
	}

	public enum BCXCallType : byte
	{
		Video = 0,
		Upgradable,
		Only
	}

	//[Native]
	public enum BCXUserStatus : long
	{
		Unknown = 0,
		Online,
		Busy,
		Offline
	}

	//[Native]
	public enum BCXCallParticipantState : long
	{
		Unknown = -1,
		Invited,
		Answered,
		Declined,
		DeclinedByDoNotDisturb,
		NoAnswer,
		Disconnected,
		TimedOut,
		Error
	}

	//[Native]
	//public enum BCXErrorCode : long
	//{
	//  NotAuthenticatedErrorCode = 1,
	//  AuthenticationErrorCode = 2,
	//  SimultaneousCallErrorCode = 3,
	//  ConnectionErrorCode = 4,
	//  ConnectionLostErrorCode = 5,
	//  NotConnectedErrorCode = 6,
	//  RequestTimedOutErrorCode = 7,
	//  MalformedResponseErrorCode = 8,
	//  DialErrorErrorCode = 9,
	//  AnswerErrorErrorCode = 10,
	//  DeclineErrorErrorCode = 11,
	//  HangUpErrorErrorCode = 12,
	//  JoinUrlErrorCode = 13,
	//  InvalidNotificationPayloadErrorCode = 14,
	//  AlreadyHandlingActionErrorCode = 15,
	//  InvalidOperationErrorCode = 16,
	//  NotSupportedActionErrorCode = 17,
	//  UnknownCallUUIDErrorCode = 18,
	//  CannotUpgradeToVideoErrorCode = 19,
	//  UnexpectedResponseErrorCode = 20,
	//  InvalidHandleErrorCode = 21,
	//  InvalidInviteErrorCode = 22,
	//  MalformedCallErrorCode = 23,
	//  GenericErrorErrorCode = 255
	//}

	//[Native]
	public enum BCHChatClientState : long
    {
        Stopped = 0,
        Starting,
        Running,
        Resuming,
        Paused,
        Failed
    }

    [Flags]
    //[Native]
    public enum BDFDDLogFlag : ulong
    {
        Error = (1uL << 0),
        Warning = (1uL << 1),
        Info = (1uL << 2),
        Debug = (1uL << 3),
        Verbose = (1uL << 4)
    }

    //[Native]
    public enum BDFDDLogLevel : ulong
    {
        Off = 0,
        Error = (BDFDDLogFlag.Error),
        Warning = (Error | BDFDDLogFlag.Warning),
        Info = (Warning | BDFDDLogFlag.Info),
        Debug = (Info | BDFDDLogFlag.Debug),
        Verbose = (Debug | BDFDDLogFlag.Verbose),
        //All = NSUIntegerMax
    }

    //[Flags]
    //[Native]
    //public enum BDFDDLogMessageOptions : long
    //{
    //  CopyFile = 1L << 0,
    //  CopyFunction = 1L << 1,
    //  DontCopyMessage = 1L << 2
    //}

    //[Native]
    //public enum BAVVideoSizeFittingMode : long
    //{
    //  ScaleToFillMode = 0,
    //  AspectFitMode = 1,
    //  AspectFillMode = 2,
    //  OriginalSizeMode = 3
    //}
}

