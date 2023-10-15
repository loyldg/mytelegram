namespace MyTelegram.Core;

////[MemoryPackable]
public partial record UnencryptedMessageResponse(long AuthKeyId,
        byte[] Data,
    //ReadOnlyMemory<byte> Data,
    string ConnectionId,
    long ReqMsgId);

////[MemoryPackable]
//public partial class UnencryptedMessageResponse
//{
//    //public UnencryptedMessageResponse()
//    //{
//    //}
//    public UnencryptedMessageResponse(long authKeyId,
//        byte[] data,
//        string connectionId,
//        long reqMsgId
//    )
//    {
//        AuthKeyId = authKeyId;
//        Data = data;
//        ConnectionId = connectionId;
//        ReqMsgId = reqMsgId;
//    }

//    public long AuthKeyId { get; set; }

//    public string ConnectionId { get; set; }

//    public byte[] Data { get; set; }

//    public bool IsAckRequest { get; set; }
//    public long ReqMsgId { get; }
//}