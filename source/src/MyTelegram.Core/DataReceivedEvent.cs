using System.Buffers;
using System.Text.Json.Serialization;

namespace MyTelegram.Core;

public record DataReceivedEvent(
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
    long AccessHashKeyId,
    long InvokeAfterMsgId
    ) : IMayHaveMemoryOwner
{
    public string ConnectionId { get; set; } = ConnectionId;
    public Guid RequestId { get; set; } = RequestId;
    public uint ObjectId { get; set; } = ObjectId;
    public long UserId { get; set; } = UserId;
    public long ReqMsgId { get; set; } = ReqMsgId;
    public int SeqNumber { get; set; } = SeqNumber;
    public long AuthKeyId { get; set; } = AuthKeyId;
    public long PermAuthKeyId { get; set; } = PermAuthKeyId;
    public ReadOnlyMemory<byte> Data { get; set; } = Data;
    public int Layer { get; set; } = Layer;
    public long Date { get; set; } = Date;
    public DeviceType DeviceType { get; set; } = DeviceType;
    public string ClientIp { get; set; } = ClientIp;
    public long SessionId { get; set; } = SessionId;
    public long AccessHashKeyId { get; set; } = AccessHashKeyId;

    public long InvokeAfterMsgId { get; set; } = InvokeAfterMsgId;

    [JsonIgnore] public IMemoryOwner<byte>? MemoryOwner { get; set; }
}