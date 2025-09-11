// ReSharper disable once CheckNamespace

namespace MyTelegram;

public record RequestInput(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long ReqMsgId,
    int SeqNumber,
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp,
    long SessionId,
    long AccessHashKeyId
) : IRequestInput
{
    public uint ObjectId { get; set; } = ObjectId;
    public int Layer { get; set; } = Layer;
    public DeviceType DeviceType { get; set; } = DeviceType;
}