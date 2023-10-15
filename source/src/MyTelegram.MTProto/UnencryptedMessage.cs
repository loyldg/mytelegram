//namespace MyTelegram.Core;

//[EventName("MyTelegram.Core.UnencryptedMessage")]
//[MemoryPackable]
namespace MyTelegram.MTProto;

public record UnencryptedMessage(
    long AuthKeyId,
    string ClientIp,
    string ConnectionId,
    //ReadOnlyMemory<byte> MessageData,
    byte[] MessageData,
    int MessageDataLength,
    long MessageId,
    uint ObjectId,
    Guid RequestId,
    long Date
    //IMemoryOwner<byte>? Owner
) : IMtpMessage //, IDisposable
{
    public string ClientIp { get; set; } = ClientIp;
    public string ConnectionId { get; set; } = ConnectionId;
}