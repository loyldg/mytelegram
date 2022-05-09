namespace MyTelegram.Core;

public record DuplicateCommandEvent(long ReqMsgId/*,string AggregateId*/, string SourceId);

//public record AckMessageReceivedEvent(long UserId, long PermAuthKeyId, long MsgId);
public record UpdateSelfPtsEvent(long SelfUserId,
    long SelfPermAuthKeyId,
    int Pts);
public record CreatePushMessageEvent(Peer ToPeer, byte[] Data, int Pts, long OnlyPushToThisAuthKeyId, PtsType PtsType/*,Dictionary<string,string> OtherData*/);

public record CreateEncryptedPushMessageEvent(long InboxOwnerPeerId, byte[] Data, int Qts, long InboxOwnerPermAuthKeyId);

public record RpcMessageHasSentEvent(long ReqMsgId, long UserId, long MsgId, long GlobalSeqNo);
public record NewPtsMessageHasSentEvent(long ToUid, Peer ToPeer, long MsgId, long TempAuthKeyId, long PermAuthKeyId, int Pts, long GlobalSeqNo, long ReqMsgId);

public record UserIsOnlineEvent(long UserId, long TempAuthKeyId, long PermAuthKeyId);

/// <summary>
/// 对Dapr发布订阅的数据进行包装,主要解决传递long数据类型最大只能2^53,超过该值后会丢失精度
/// </summary>
public record DaprPubSubWrappedEventData(string EventData, string EventType);

public record SetSessionPasswordStateEvent(long UserId, PasswordState PasswordState);

public record ChatMemberChangedEvent(long ChatId,
    MemberStateChangeType MemberStateChangeType,
    IReadOnlyList<long> MemberUidList);

public record ChannelMemberChangedEvent(long ChannelId,
    MemberStateChangeType MemberStateChangeType,
    IReadOnlyList<long> MemberUidList);

public record NewDeviceCreatedEvent(long ReqMsgId,
    long PermAuthKeyId,
    long TempAuthKeyId,
    long UserId,
    int AppId,
    string AppName,
    string AppVersion,
    long Hash,
    bool OfficialApp,
    bool PasswordPending,
    string DeviceModel,
    string Platform,
    string SystemVersion,
    string SystemLangCode,
    string LangPack,
    string LangCode,
    string Ip,
    int Layer);

public record UserSignInSuccessEvent(long TempAuthKeyId,
    long PermAuthKeyId,
    long UserId, PasswordState PasswordState);

public record UserSignUpSuccessIntegrationEvent(long TempAuthKeyId,
    long PermAuthKeyId,
    long UserId);

public record UnRegisterAuthKeyEvent(long PermAuthKeyId);

public record SessionPasswordStateChangedEvent(long AuthKeyId, PasswordState PasswordState);

public record BindUidToSessionEvent(long UserId,
    long AuthKeyId, long PermAuthKeyId);

public record BindUidToAuthKeyIntegrationEvent(long AuthKeyId,
    long PermAuthKeyId,
    long UserId);

public record AuthKeyUnRegisteredIntegrationEvent(long PermAuthKeyId,
    long TempAuthKeyId);

public record AuthKeyCreatedIntegrationEvent(byte[] Data,
    byte[] ServerSalt,
    bool IsPermanent);

public interface ISessionMessage { }

public record PushMessageToPeerEvent(int PeerType,
    long PeerId,
    byte[] Data,
    long ExcludeAuthKeyId,
    long ExcludeUid,
    long OnlySendToThisAuthKeyId, int Pts, PtsType PtsType, long GlobalSeqNo) : ISessionMessage;

public record PushSessionMessageToAuthKeyIdEvent(long AuthKeyId,
    byte[] Data,
    int Pts,
    PtsType PtsType, long GlobalSeqNo) : ISessionMessage;

public record PushSessionMessageToPeerEvent(int PeerType,
    long PeerId,
    byte[] Data,
    long ExcludeAuthKeyId,
    long ExcludeUid,
    long OnlySendToThisAuthKeyId, int Pts,
    PtsType PtsType, long GlobalSeqNo) : ISessionMessage;

public record FileDataResultResponseReceivedEvent(long ReqMsgId,
    byte[] Data) : ISessionMessage;

public record DataResultResponseReceivedEvent(long ReqMsgId,
    byte[] Data) : ISessionMessage;

public record DataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data);

public record AcksDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record UpdatesDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record UploadDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record DownloadDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record PhoneCallDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record MessengerDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);

public record PushDataReceivedEvent(uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data) : DataReceivedEvent(ObjectId, UserId, ReqMsgId, SeqNumber, AuthKeyId, PermAuthKeyId,
    Data);