//namespace MyTelegram.Core;

////[MemoryPackable]
namespace MyTelegram.Core;

public partial record EncryptedMessage(long AuthKeyId,
    byte[] MsgKey,
    byte[] EncryptedData,
    //ReadOnlyMemory<byte> MsgKey,
    //ReadOnlyMemory<byte> EncryptedData,
    string ConnectionId,
    string? ClientIp,
    Guid RequestId,
    long Date
)
{ 
    public string? ClientIp { get; set; } = ClientIp; 
} 