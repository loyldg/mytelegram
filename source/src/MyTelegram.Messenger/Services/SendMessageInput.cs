namespace MyTelegram.Messenger.Services;

public record SendMessageInput
{
    public SendMessageInput(RequestInfo requestInfo,
        long senderUserId,
        Peer toPeer,
        string message,
        long randomId,
        //byte[]? entities = null,
        TVector<IMessageEntity>? entities = null,
        //int? replyToMsgId = null,
        //IReplyTo? replyTo = null,
        IInputReplyTo? inputReplyTo = null,
        bool clearDraft = false,
        //byte[]? media = null,
        IMessageMedia? media = null,
        IInputMedia? inputMedia = null,
        //Peer? peer = null,
        SendMessageType sendMessageType = SendMessageType.Text,
        MessageType messageType = MessageType.Text,
        //MessageActionType messageActionType = MessageActionType.None,
        //string? messageActionData = null,
        IMessageAction? messageAction = null,
        long? groupId = null,
        int groupItemCount = 1,
        long? pollId = null,
        IReplyMarkup? replyMarkup = null,
        int? topMsgId = null,
        Peer? sendAs = null,
        //string? quickReplyShortcut = null,
        IInputQuickReplyShortcut? inputQuickReplyShortcut = null,
        long? effect = null,
        bool isSendGroupedMessage = false,
        bool isSendQuickReplyMessage = false,
        bool silent = false,
        int? scheduleDate = null,
        bool invertMedia = false
        )
    {
        RequestInfo = requestInfo;
        SendMessageType = sendMessageType;
        MessageType = messageType;
        MessageAction = messageAction;
        //MessageActionType = messageActionType;
        SenderUserId = senderUserId;
        //Peer = peer;
        ToPeer = toPeer;
        Message = message;
        RandomId = randomId;
        //ReplyToMsgId = replyToMsgId;
        //MessageActionData = messageActionData;
        Entities = entities;
        InputReplyTo = inputReplyTo;
        ClearDraft = clearDraft;
        Media = media;
        InputMedia = inputMedia;
        GroupId = groupId;
        GroupItemCount = groupItemCount;
        PollId = pollId;
        ReplyMarkup = replyMarkup;
        TopMsgId = topMsgId;
        SendAs = sendAs;
        InputQuickReplyShortcut = inputQuickReplyShortcut;
        Effect = effect;
        IsSendGroupedMessage = isSendGroupedMessage;
        IsSendQuickReplyMessage = isSendQuickReplyMessage;
        Silent = silent;
        ScheduleDate = scheduleDate;
        InvertMedia = invertMedia;
    }

    public bool ClearDraft { get; }
    public TVector<IMessageEntity>? Entities { get; }
    public IInputReplyTo? InputReplyTo { get; }
    //public IReplyTo? ReplyTo { get; }
    public long? GroupId { get; }
    public int GroupItemCount { get; } = 1;
    public long? PollId { get; }
    public IReplyMarkup? ReplyMarkup { get; }
    public int? TopMsgId { get; }
    public Peer? SendAs { get; }
    public IInputQuickReplyShortcut? InputQuickReplyShortcut { get; }
    public long? Effect { get; }
    public bool IsSendGroupedMessage { get; }
    public bool IsSendQuickReplyMessage { get; }
    public bool Silent { get; }
    public int? ScheduleDate { get; }
    public bool InvertMedia { get; }
    public IMessageMedia? Media { get; }
    public IInputMedia? InputMedia { get; }

    public string Message { get; }
    //public string? MessageActionData { get; }
    //public MessageActionType MessageActionType { get; }
    public MessageType MessageType { get; }

    public IMessageAction? MessageAction { get; }

    //public Peer? Peer { get; }
    public long RandomId { get; }
    //public int? ReplyToMsgId { get; }
    public long SenderUserId { get; }
    public RequestInfo RequestInfo { get; }
    public SendMessageType SendMessageType { get; }
    public Peer ToPeer { get; }
}
