namespace MyTelegram.Core;

public record AcksDataReceivedEvent(
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
    //byte[] Data,
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
    public static AcksDataReceivedEvent Create()
    {
        return new AcksDataReceivedEvent(string.Empty, ConnectionType.UnKnown, Guid.Empty, 0, 0, 0, 0, 0,
            0, default, 0,
            0, DeviceType.Unknown, string.Empty, 0, 0);
    }
}