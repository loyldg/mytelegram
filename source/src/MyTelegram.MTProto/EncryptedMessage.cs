//namespace MyTelegram.MTProto;

// ReSharper disable once CheckNamespace
//namespace MyTelegram.Core;

//[EventName("MyTelegram.Core.EncryptedMessage")]

namespace MyTelegram.MTProto;

public record EncryptedMessage(long AuthKeyId,
    byte[] MsgKey,
    byte[] EncryptedData,
    //ReadOnlyMemory<byte> MsgKey,
    //ReadOnlyMemory<byte> EncryptedData,
    // ReadOnlyMemory<byte> can not be serialized
    string ConnectionId,
    string ClientIp,
    Guid RequestId,
    long Date
    //,
    //IMemoryOwner<byte>? Owner
) : IMtpMessage //, IDisposable
{
    //public long AuthKeyId { get; set; } = AuthKeyId;

    public string ClientIp { get; set; } = ClientIp;
    public string ConnectionId { get; set; } = ConnectionId;
}
