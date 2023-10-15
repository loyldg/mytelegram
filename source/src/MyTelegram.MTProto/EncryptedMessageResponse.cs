//namespace MyTelegram.Core;

//[EventName("MyTelegram.Core.EncryptedMessageResponse")]
namespace MyTelegram.MTProto;

public record EncryptedMessageResponse(long AuthKeyId,
    byte[] Data,
    //ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long SeqNumber);
