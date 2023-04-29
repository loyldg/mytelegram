namespace MyTelegram.Core;

//[EventName("MyTelegram.Core.EncryptedMessageResponse")]
public record EncryptedMessageResponse(long AuthKeyId,
    byte[] Data,
    //ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long SeqNumber);
