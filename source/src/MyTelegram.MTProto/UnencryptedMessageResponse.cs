namespace MyTelegram.MTProto;

//[EventName("MyTelegram.Core.UnencryptedMessageResponse")]
public record UnencryptedMessageResponse(long AuthKeyId,
    byte[] Data,
    //ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long ReqMsgId);
