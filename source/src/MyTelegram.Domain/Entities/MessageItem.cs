namespace MyTelegram.Domain.Entities;

public class MessageItem :ValueObject// Entity<MessageId>
{
    public MessageItem(/*MessageId id,*/
        Peer ownerPeer,
        Peer toPeer,
        Peer senderPeer,
        int messageId,
        string message,
        int date,
        long randomId,
        bool isOut,
        SendMessageType sendMessageType = SendMessageType.Text,
        MessageType messageType = MessageType.Text,
        MessageSubType messageSubType = MessageSubType.Normal,
        int? replyToMsgId = null,
        string? messageActionData = null,
        MessageActionType messageActionType = MessageActionType.None,
        byte[]? entities = null,
        byte[]? media = null,
        long? groupId = null,
        //int? groupItemCount = null,
        bool post = false,
        MessageFwdHeader? fwdHeader = null,
        int? views = null,
        long? pollId=null
    ) //: base(id)
    {
        OwnerPeer = ownerPeer;
        ToPeer = toPeer;
        SenderPeer = senderPeer;
        MessageId = messageId;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Date = date;
        MessageType = messageType;
        MessageSubType = messageSubType;
        RandomId = randomId;
        IsOut = isOut;
        SendMessageType = sendMessageType;
        ReplyToMsgId = replyToMsgId;
        MessageActionData = messageActionData;
        MessageActionType = messageActionType;
        FwdHeader = fwdHeader;
        Entities = entities;
        Media = media;
        GroupId = groupId;
        Post = post;
        //GroupItemCount = groupItemCount;
        Views = views;
        PollId = pollId;
    }

    public int Date { get; }
    public byte[]? Entities { get; }
    public MessageFwdHeader? FwdHeader { get; }
    public long? GroupId { get; }


    //public int? GroupItemCount { get; }
    public byte[]? Media { get; }
    public string Message { get; }
    public string? MessageActionData { get; }
    public MessageActionType MessageActionType { get; }
    public MessageType MessageType { get; }
    public MessageSubType MessageSubType { get; }
    public int MessageId { get; internal set; }
    public Peer OwnerPeer { get; }
    public Peer ToPeer { get; }
    public Peer SenderPeer { get; }
    public long RandomId { get; }
    public bool IsOut { get; }
    public SendMessageType SendMessageType { get; }
    public int? ReplyToMsgId { get; }

    public bool Post { get; internal set; }
    public int? Views { get; internal set; }
    public long? PollId { get; }
}