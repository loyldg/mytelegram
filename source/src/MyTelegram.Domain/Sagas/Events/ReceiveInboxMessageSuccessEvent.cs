//namespace MyTelegram.Domain.Sagas.Events;

//public class ReceiveInboxMessageSuccessEvent : AggregateEvent<MessageSaga, MessageSagaId>
//{
//    //public int DeletedUid { get; }
//    //public Dictionary<string, string> ExtraData { get; }

//    public ReceiveInboxMessageSuccessEvent(
//        long ownerPeerId,
//        long senderPeerId,
//        int messageId,
//        int pts,
//        string message,
//        int date,
//        Peer toPeer,
//        byte[]? entities,
//        int? replyToMsgId,
//        SendMessageType sendMessageType,
//        MessageBoxType messageBoxType,
//        string? messageActionData,
//        MessageBoxSubType subType,
//        MessageFwdHeader? fwdHeader,
//        string title,
//        byte[]? media,
//        long? groupId,
//        int? views,
//        bool post
//        //Dictionary<string, string> extraData
//    )
//    {
//        OwnerPeerId = ownerPeerId;
//        SenderPeerId = senderPeerId;
//        MessageId = messageId;
//        Pts = pts;
//        Message = message;
//        Date = date;
//        ToPeer = toPeer;
//        Entities = entities;
//        ReplyToMsgId = replyToMsgId;
//        SendMessageType = sendMessageType;
//        MessageBoxType = messageBoxType;
//        MessageActionData = messageActionData;
//        SubType = subType;
//        FwdHeader = fwdHeader;
//        //ExtraData = extraData;
//        Title = title;
//        Media = media;
//        GroupId = groupId;
//        Views = views;
//        Post = post;
//    }

//    public int Date { get; }
//    public byte[]? Entities { get; }
//    public MessageFwdHeader? FwdHeader { get; }

//    public long? GroupId { get; }

//    public byte[]? Media { get; }
//    public string Message { get; }
//    public string? MessageActionData { get; }
//    public MessageBoxType MessageBoxType { get; }
//    public int MessageId { get; }
//    public long OwnerPeerId { get; }

//    public bool Post { get; }
//    public int Pts { get; }
//    public int? ReplyToMsgId { get; }
//    public long SenderPeerId { get; }
//    public SendMessageType SendMessageType { get; }
//    public MessageBoxSubType SubType { get; }
//    public string Title { get; }
//    public Peer ToPeer { get; }

//    public int? Views { get; }
//}
