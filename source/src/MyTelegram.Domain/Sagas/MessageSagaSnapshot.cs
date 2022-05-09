//using System.Collections.Concurrent;

//namespace MyTelegram.Domain.Sagas;

//public class MessageSagaSnapshot : ISnapshot
//{
//    public MessageSagaSnapshot(Peer inboxToPeer,
//        int receiverCount,
//        int inboxCount,
//        int? views,
//        bool post,
//        //ConcurrentDictionary<int, int> ptsDict,
//        ConcurrentDictionary<long, int> boxDict,
//        OutboxCreatedEvent outbox,
//        string tile,
//        long deletedChatUserId,
//        bool needWaitForStartChannelMessage,
//        bool receivedStartChannelMessage,
//        IReadOnlyList<long> botUidList,
//        Dictionary<long, InboxItem> inboxItems,
//        IReadOnlyList<long> chatMemberUidList,
//        bool needWaitForReplyToMsgId,
//        bool receivedReplyToMsgId,
//        long linkedChannelId)
//    {
//        InboxToPeer = inboxToPeer;
//        ReceiverCount = receiverCount;
//        InboxCount = inboxCount;
//        Views = views;
//        Post = post;
//        //PtsDict = ptsDict;
//        BoxDict = boxDict;
//        Outbox = outbox;
//        Tile = tile;
//        DeletedChatUserId = deletedChatUserId;
//        NeedWaitForStartChannelMessage = needWaitForStartChannelMessage;
//        ReceivedStartChannelMessage = receivedStartChannelMessage;
//        BotUidList = botUidList;
//        InboxItems = inboxItems;
//        ChatMemberUidList = chatMemberUidList;
//        NeedWaitForReplyToMsgId = needWaitForReplyToMsgId;
//        ReceivedReplyToMsgId = receivedReplyToMsgId;
//        LinkedChannelId = linkedChannelId;
//    }

//    public IReadOnlyList<long> BotUidList { get; }

//    public ConcurrentDictionary<long, int> BoxDict { get; }
//    public IReadOnlyList<long> ChatMemberUidList { get; }
//    public long DeletedChatUserId { get; }
//    public int InboxCount { get; }
//    public Dictionary<long, InboxItem> InboxItems { get; }
//    public Peer InboxToPeer { get; }
//    public long LinkedChannelId { get; }
//    public bool NeedWaitForReplyToMsgId { get; }
//    public bool NeedWaitForStartChannelMessage { get; }

//    //private bool _outboxEventHasEmit;

//    internal OutboxCreatedEvent Outbox { get; }
//    public bool Post { get; }

//    //public ConcurrentDictionary<long, int> PtsDict { get; }
//    public bool ReceivedReplyToMsgId { get; }
//    public bool ReceivedStartChannelMessage { get; }
//    public int ReceiverCount { get; }
//    public string Tile { get; }
//    public int? Views { get; }
//}
