namespace MyTelegram.Domain.ValueObjects;

public class MessageItem : ValueObject
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
        long? pollId = null,
        byte[]? replyMarkup = null,
        long? linkedChannelId = null,
        int? topMsgId = null,
        string? postAuthor = null
    )// : base(id)
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
        ReplyMarkup = replyMarkup;
        LinkedChannelId = linkedChannelId;
        TopMsgId = topMsgId;
        PostAuthor = postAuthor;
    }

    public int Date { get; init; }
    public byte[]? Entities { get; init; }
    public MessageFwdHeader? FwdHeader { get; init; }
    public long? GroupId { get; init; }


    //public int? GroupItemCount { get; }
    public byte[]? Media { get; init; }
    public string Message { get; init; }
    public string? MessageActionData { get; init; }
    public MessageActionType MessageActionType { get; init; }
    public MessageType MessageType { get; init; }
    public MessageSubType MessageSubType { get; init; }
    public int MessageId { get; internal set; }
    public Peer OwnerPeer { get; init; }
    public Peer ToPeer { get; init; }
    public Peer SenderPeer { get; set; }
    public long RandomId { get; init; }
    public bool IsOut { get; init; }
    public SendMessageType SendMessageType { get; init; }
    public int? ReplyToMsgId { get; init; }

    public bool Post { get; internal set; }
    public int? Views { get; internal set; }
    public long? PollId { get; init; }
    public byte[]? ReplyMarkup { get; init; }
    public long? LinkedChannelId { get; internal set; }
    public int? TopMsgId { get; }
    public string? PostAuthor { get; }
}
