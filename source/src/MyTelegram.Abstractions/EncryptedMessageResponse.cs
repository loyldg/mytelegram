namespace MyTelegram.Abstractions;

/// <summary>
/// 
/// </summary>
/// <param name="AuthKeyId"></param>
/// <param name="Data">Encrypted Message https://corefork.telegram.org/mtproto/description#encrypted-message</param>
/// <param name="ConnectionId"></param>
/// <param name="SeqNumber"></param>
public record EncryptedMessageResponse(long AuthKeyId,
    ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long SeqNumber
) : IMayHaveMemoryOwner
{
    public IMemoryOwner<byte>? MemoryOwner { get; set; }
}