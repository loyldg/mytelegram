namespace MyTelegram.Messenger.Services;

public record SendMessageInput
{
    public SendMessageInput(RequestInfo requestInfo,
        long senderPeerId,
        Peer toPeer,
        string message,
        long randomId,
        //byte[]? entities = null,
        IList<IMessageEntity>? entities = null,
        int? replyToMsgId = null,
        bool clearDraft = false,
        byte[]? media = null,
        //Peer? peer = null,
        SendMessageType sendMessageType = SendMessageType.Text,
        MessageType messageType = MessageType.Text,
        //MessageActionType messageActionType = MessageActionType.None,
        string? messageActionData = null,
        long groupId = 0,
        int groupItemCount = 1,
        long? pollId = null,
        byte[]? replyMarkup = null,
        int? topMsgId = null
        )
    {
        RequestInfo = requestInfo;
        SendMessageType = sendMessageType;
        MessageType = messageType;
        //MessageActionType = messageActionType;
        SenderPeerId = senderPeerId;
        //Peer = peer;
        ToPeer = toPeer;
        Message = message;
        RandomId = randomId;
        ReplyToMsgId = replyToMsgId;
        MessageActionData = messageActionData;
        Entities = entities;
        ClearDraft = clearDraft;
        Media = media;
        GroupId = groupId;
        GroupItemCount = groupItemCount;
        PollId = pollId;
        ReplyMarkup = replyMarkup;
        TopMsgId = topMsgId;
    }

    public bool ClearDraft { get; }
    public IList<IMessageEntity>? Entities { get; }
    public long GroupId { get; }
    public int GroupItemCount { get; } = 1;
    public long? PollId { get; }
    public byte[]? ReplyMarkup { get; }
    public int? TopMsgId { get; }
    public byte[]? Media { get; }
    public string Message { get; }
    public string? MessageActionData { get; }
    //public MessageActionType MessageActionType { get; }
    public MessageType MessageType { get; }
    //public Peer? Peer { get; }
    public long RandomId { get; }
    public int? ReplyToMsgId { get; }
    public long SenderPeerId { get; }
    public RequestInfo RequestInfo { get; }
    public SendMessageType SendMessageType { get; }
    public Peer ToPeer { get; }
}
