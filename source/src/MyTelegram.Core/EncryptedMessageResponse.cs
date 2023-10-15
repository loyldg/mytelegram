namespace MyTelegram.Core;

////[MemoryPackable]
public partial record EncryptedMessageResponse(long AuthKeyId,
        byte[] Data,
    //ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long SeqNumber);