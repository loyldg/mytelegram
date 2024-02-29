namespace MyTelegram.Core;

public record DuplicateCommandEvent(long UserId, long ReqMsgId);

//public record AckMessageReceivedEvent(long UserId, long PermAuthKeyId, long MsgId);
public record UpdateSelfPtsEvent(long SelfUserId,
    long SelfPermAuthKeyId,
    int Pts);

public record CreatePushMessageEvent(Peer ToPeer,
    byte[] Data,
    int Pts,
    long OnlyPushToThisAuthKeyId,
    PtsType PtsType /*,Dictionary<string,string> OtherData*/);

public record CreateEncryptedPushMessageEvent(long InboxOwnerPeerId,
    byte[] Data,
    int Qts,
    long InboxOwnerPermAuthKeyId);

public record RpcMessageHasSentEvent(long ReqMsgId,
    long UserId,
    long MsgId,
    long GlobalSeqNo);

public record NewPtsMessageHasSentEvent(long ToUid,
    Peer ToPeer,
    long MsgId,
    long TempAuthKeyId,
    long PermAuthKeyId,
    int Pts,
    long GlobalSeqNo,
    long ReqMsgId);

public record UserIsOnlineEvent(long UserId,
    long TempAuthKeyId,
    long PermAuthKeyId);

public record SetSessionPasswordStateEvent(long UserId,
    PasswordState PasswordState);

public record ChatMemberChangedEvent(long ChatId,
    MemberStateChangeType MemberStateChangeType,
    IReadOnlyList<long> MemberUidList);

public record ChannelMemberChangedEvent(long ChannelId,
    MemberStateChangeType MemberStateChangeType,
    IReadOnlyList<long> MemberUidList);

public partial record NewDeviceCreatedEvent(RequestInfo RequestInfo,
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
    long UserId,
    PasswordState PasswordState);

public record UserSignUpSuccessIntegrationEvent(long TempAuthKeyId,
    long PermAuthKeyId,
    long UserId);

public record UnRegisterAuthKeyEvent(long PermAuthKeyId);

public record SessionPasswordStateChangedEvent(long AuthKeyId,
    PasswordState PasswordState);

public record BindUidToSessionEvent(long UserId,
    long AuthKeyId,
    long PermAuthKeyId);

public record BindUidToAuthKeyIntegrationEvent(long AuthKeyId,
    long PermAuthKeyId,
    long UserId);

public record AuthKeyUnRegisteredIntegrationEvent(long PermAuthKeyId,
    long TempAuthKeyId);

public record AuthKeyCreatedIntegrationEvent(byte[] Data,
    long ServerSalt,
    bool IsPermanent);

public interface ISessionMessage
{
}

public record PushMessageToPeerEvent(int PeerType,
    long PeerId,
    byte[] Data,
    long ExcludeAuthKeyId,
    long ExcludeUid,
    long OnlySendToThisAuthKeyId,
    int Pts,
    PtsType PtsType,
    long GlobalSeqNo) : ISessionMessage;

public partial record LayeredAuthKeyIdMessageCreatedIntegrationEvent(
    long AuthKeyId, byte[] Data, int Pts, long GlobalSeqNo, LayeredData<byte[]>? LayeredData) : ISessionMessage;

//[MemoryPackable]
public record LayeredPushMessageCreatedIntegrationEvent(int PeerType, long PeerId, byte[] Data, long? ExcludeAuthKeyId,
    long? ExcludeUserId, long? OnlySendToUserId, long? OnlySendToThisAuthKeyId, int Pts, long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData) : ISessionMessage;

//[MemoryPackable]
public record LayeredPushMessageCreatedIntegrationEvent<TExtraData>(int PeerType,
    long PeerId,
    byte[] Data,
    long? ExcludeAuthKeyId,
    long? ExcludeUserId,
    long? OnlySendToUserId,
    long? OnlySendToThisAuthKeyId,
    int Pts,
    //PtsType PtsType,
    //UpdatesType UpdatesType,
    long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData, TExtraData ExtraData) :
    LayeredPushMessageCreatedIntegrationEvent(PeerType, PeerId, Data, ExcludeAuthKeyId, ExcludeUserId,
        OnlySendToUserId,
        OnlySendToThisAuthKeyId, Pts, GlobalSeqNo, LayeredData);

//[MemoryPackable] 
public record ChannelReactionChangedData(Dictionary<long, LayeredData<byte[]>> LayeredData);

//[MemoryPackable]
public class EmptyLayeredData
{
    public static EmptyLayeredData Empty { get; } = new();
}

public record FileDataResultResponseReceivedEvent(long ReqMsgId,
    byte[] Data) : ISessionMessage;

public record DataResultResponseReceivedEvent(long ReqMsgId,
    //ReadOnlyMemory<byte> Data
    byte[] Data
) : ISessionMessage;

public record DataResultResponseWithUserIdReceivedEvent(long ReqMsgId, byte[] Data, long UserId, long AuthKeyId,
    long PermAuthKeyId) : ISessionMessage;

public partial record DataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data,
    //byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
);

public record AcksDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record DomainEventMessage(string EventId, string Message, IReadOnlyDictionary<string, string> Headers);

public record UpdatesDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record UploadDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record DownloadDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record PhoneCallDataReceivedEvent(uint ObjectId, long UserId, long ReqMsgId, int SeqNumber, long AuthKeyId,
    long PermAuthKeyId, byte[] Data, int Layer);

public record MessengerDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record MessengerQueryDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
    ) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record MessengerCommandDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
    ) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);

public record PushDataReceivedEvent(uint ObjectId, long UserId, long ReqMsgId, int SeqNumber, long AuthKeyId,
    long PermAuthKeyId, byte[] Data, int Layer);

public record StickerDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);