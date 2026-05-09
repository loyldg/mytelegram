// ReSharper disable once CheckNamespace

using MyTelegram.Abstractions;

namespace MyTelegram;

public record RequestInput(
    string ConnectionId,
    ConnectionType ConnectionType,
    //int DcId,
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
    long AccessHashKeyId,
    long InvokeAfterMsgId,
    ConnectedBotData? ConnectedBotData = null
) : IRequestInput
{
    public uint ObjectId { get; set; } = ObjectId;
    public int Layer { get; set; } = Layer;
    public DeviceType DeviceType { get; set; } = DeviceType;

    public long UserId { get; set; } = UserId;
    public long AccessHashKeyId { get; set; } = AccessHashKeyId;
    public ConnectedBotData? ConnectedBotData { get; set; } = ConnectedBotData;
    public long InvokeAfterMsgId { get; set; } = InvokeAfterMsgId;
}