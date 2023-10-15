namespace MyTelegram.Core;

////[MemoryPackable]

public partial record UnencryptedMessage(long AuthKeyId,
    string ClientIp,
    string ConnectionId,
    //ReadOnlyMemory<byte> MessageData,
    byte[] MessageData,
    int MessageDataLength,
    long MessageId,
    uint ObjectId,
    Guid RequestId,
    long Date
    )
{
    public string ClientIp { get; set; } = ClientIp;
    public string ConnectionId { get; set; } = ConnectionId;
}

//////[MemoryPackable]
////public partial class UnencryptedMessage
////{
////    public UnencryptedMessage(long authKeyId,
////        string? clientIp,
////        string? connectionId,
////        byte[] messageData,
////        int messageDataLength,
////        long messageId,
////        uint objectId)
////    {
////        AuthKeyId = authKeyId;
////        ClientIp = clientIp;
////        ConnectionId = connectionId;
////        MessageData = messageData;
////        MessageDataLength = messageDataLength;
////        MessageId = messageId;
////        ObjectId = objectId;
////    }

////    public long AuthKeyId { get; init; }
////    public string? ClientIp { get; set; }
////    public string? ConnectionId { get; set; }
////    public byte[] MessageData { get; init; }
////    public int MessageDataLength { get; init; }
////    public long MessageId { get; init; }
////    public uint ObjectId { get; init; }
////}