namespace MyTelegram.Core;

public record UpdatesDataReceivedEvent(
    string ConnectionId,
    ConnectionType ConnectionType,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    ReadOnlyMemory<byte> Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp,
    long SessionId,
    long AccessHashKeyId,
    long InvokeAfterMsgId
) : DataReceivedEvent(
    ConnectionId,
    ConnectionType,
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
    ClientIp,
    SessionId,
    AccessHashKeyId,
    InvokeAfterMsgId
)
{
    public static UpdatesDataReceivedEvent Create()
    {
        return new UpdatesDataReceivedEvent(string.Empty, ConnectionType.UnKnown, Guid.Empty, 0, 0, 0, 0, 0,
            0, default, 0,
            0, DeviceType.Unknown, string.Empty, 0, 0, 0);
    }
}