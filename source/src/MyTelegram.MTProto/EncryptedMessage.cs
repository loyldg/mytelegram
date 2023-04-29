//namespace MyTelegram.MTProto;

namespace MyTelegram.Core;

//[EventName("MyTelegram.Core.EncryptedMessage")]
public record EncryptedMessage(long AuthKeyId,
    byte[] MsgKey,
    byte[] EncryptedData,
    //ReadOnlyMemory<byte> MsgKey,
    //ReadOnlyMemory<byte> EncryptedData,
    // ReadOnlyMemory<byte> can not be serialized
    string ConnectionId,
    string? ClientIp,
    Guid RequestId //,
    //IMemoryOwner<byte>? Owner
) : IMtpMessage //, IDisposable
{
    //public long AuthKeyId { get; set; } = AuthKeyId;

    public string? ClientIp { get; set; } = ClientIp;
    public string ConnectionId { get; set; } = ConnectionId;
}
