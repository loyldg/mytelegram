namespace MyTelegram.Core;

public record EncryptedMessageResponse(long AuthKeyId,
    byte[] Data,
    string ConnectionId,
    long SeqNumber);