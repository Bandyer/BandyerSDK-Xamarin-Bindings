// Copyright © 2020 Bandyer. All rights reserved.
// See LICENSE for licensing information

using System;
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
	
	public enum BCXCallClientState : long
	{
		Stopped = 0,
		Starting,
		Running,
		Resuming,
		Paused,
		Reconnecting
	}
	
	public enum BCXCallDirection : long
	{
		Incoming = 0,
		Outgoing
	}

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
		AudioVideo = 0,
		AudioUpgradable,
		AudioOnly
	}
	
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
	
	public enum BCXUserStatus : long
	{
		Unknown = 0,
		Online,
		Busy,
		Offline
	}
	
	public enum BCXErrorCode : long
	{
		NotAuthenticatedErrorCode = 1,
		AuthenticationErrorCode = 2,
		SimultaneousCallErrorCode = 3,
		ConnectionErrorCode = 4,
		ConnectionLostErrorCode = 5,
		NotConnectedErrorCode = 6,
		RequestTimedOutErrorCode = 7,
		MalformedResponseErrorCode = 8,
		DialErrorErrorCode = 9,
		AnswerErrorErrorCode = 10,
		DeclineErrorErrorCode = 11,
		HangUpErrorErrorCode = 12,
		JoinUrlErrorCode = 13,
		InvalidNotificationPayloadErrorCode = 14,
		AlreadyHandlingActionErrorCode = 15,
		InvalidOperationErrorCode = 16,
		NotSupportedActionErrorCode = 17,
		UnknownCallUUIDErrorCode = 18,
		CannotUpgradeToVideoErrorCode = 19,
		UnexpectedResponseErrorCode = 20,
		InvalidHandleErrorCode = 21,
		InvalidInviteErrorCode = 22,
		MalformedCallErrorCode = 23,
		CouldNotFindAnOnGoingCallErrorCode = 24,
		AnotherCallOngoingErrorCode = 25,
		CallNotConnectedErrorCode = 26,
		GenericErrorErrorCode = 255
	}
	
	public enum BDKSpeakerHijackingStrategy : ulong
	{
		Never = 0,
		Always = 1,
		Video = 2,
		VideoForeground = 3
	}
	
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
	public enum BDFDDLogFlag : ulong
	{
		Error = (1uL << 0),
		Warning = (1uL << 1),
		Info = (1uL << 2),
		Debug = (1uL << 3),
		Verbose = (1uL << 4)
	}
	
	public enum BDFDDLogLevel : ulong
	{
		Off = 0,
		Error = (BDFDDLogFlag.Error),
		Warning = (Error | BDFDDLogFlag.Warning),
		Info = (Warning | BDFDDLogFlag.Info),
		Debug = (Info | BDFDDLogFlag.Debug),
		Verbose = (Debug | BDFDDLogFlag.Verbose),
		//All = (9223372036854775807L * 2 + 1)
	}
	
	public enum BDKCallPresentationErrorCode : long
	{
		UnsupportedIntentProvided = 0,
		MissingCallViewControllerConfiguration = 1,
		OpenDownloadsViewWithoutAnOngoingCall = 2,
		AnotherCallOnGoing = 3,
		MissingCall = 4
	}
}
