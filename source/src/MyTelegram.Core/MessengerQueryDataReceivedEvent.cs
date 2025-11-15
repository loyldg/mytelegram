namespace MyTelegram.Core;

public record MessengerQueryDataReceivedEvent(
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
    long AccessHashKeyId
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
    AccessHashKeyId
)
{
    public static MessengerQueryDataReceivedEvent Create()
    {
        return new MessengerQueryDataReceivedEvent(string.Empty, ConnectionType.UnKnown, Guid.Empty, 0, 0, 0, 0, 0,
            0, default, 0,
            0, DeviceType.Unknown, string.Empty, 0, 0);
    }
}