//using MyTelegram.Domain.Events;

//namespace MyTelegram.Domain.Sagas.Events;

//public class SendOutboxMessageSuccessEvent : AggregateEvent<MessageSaga, MessageSagaId>, IHasCorrelationId
//{
//    public SendOutboxMessageSuccessEvent(
//        long reqMsgId,
//        long selfAuthKeyId,
//        long selfPermAuthKeyId,
//        long ownerPeerId,
//        long senderPeerId,
//        int messageId,
//        int pts,
//        string message,
//        int date,
//        Peer toPeer,
//        byte[]? entities,
//        int? replyToMsgId,
//        long randomId,
//        SendMessageType sendMessageType,
//        MessageBoxType messageBoxType,
//        MessageBoxSubType subType,
//        bool post,
//        int? views,
//        string? messageActionData,
//        Guid correlationId,
//        MessageFwdHeader? fwdHeader,
//        byte[]? media,
//        long? groupId,
//        int groupItemCount
//    )
//    {
//        ReqMsgId = reqMsgId;
//        SelfAuthKeyId = selfAuthKeyId;
//        SelfPermAuthKeyId = selfPermAuthKeyId;
//        OwnerPeerId = ownerPeerId;
//        SenderPeerId = senderPeerId;
//        MessageId = messageId;
//        Pts = pts;
//        Message = message;
//        Date = date;
//        ToPeer = toPeer;
//        Entities = entities;
//        ReplyToMsgId = replyToMsgId;
//        RandomId = randomId;
//        SendMessageType = sendMessageType;
//        MessageBoxType = messageBoxType;
//        SubType = subType;
//        Post = post;
//        Views = views;
//        MessageActionData = messageActionData;
//        CorrelationId = correlationId;
//        FwdHeader = fwdHeader;
//        Media = media;
//        GroupId = groupId;
//        GroupItemCount = groupItemCount;
//    }

//    public int Date { get; }
//    public byte[]? Entities { get; }
//    public MessageFwdHeader? FwdHeader { get; }
//    public long? GroupId { get; }
//    public int GroupItemCount { get; }
//    public byte[]? Media { get; }
//    public string Message { get; }
//    public string? MessageActionData { get; }
//    public MessageBoxType MessageBoxType { get; }

//    public int MessageId { get; }

//    //public long SenderAuthKeyId { get; }
//    public long OwnerPeerId { get; }
//    public bool Post { get; }
//    public int Pts { get; }
//    public long RandomId { get; }
//    public int? ReplyToMsgId { get; }

//    public long ReqMsgId { get; }
//    public long SelfAuthKeyId { get; }
//    public long SelfPermAuthKeyId { get; }
//    public long SenderPeerId { get; }
//    public SendMessageType SendMessageType { get; }
//    public MessageBoxSubType SubType { get; }
//    public Peer ToPeer { get; }
//    public int? Views { get; }
//    public Guid CorrelationId { get; }
//}
