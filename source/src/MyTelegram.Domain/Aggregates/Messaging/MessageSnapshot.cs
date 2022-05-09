namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageSnapshot : ISnapshot
{
    public RequestInfo Request { get; }

    public MessageItem? MessageItem { get; }

    public int SenderMessageId { get; }

    public bool ClearDraft { get; }

    public int GroupItemCount { get; }

    public Dictionary<long, int> InboxItems { get; }
    public string? ChatTitle { get; }

    public IReadOnlyList<long>? ChatMemberUidList { get; }
    public IReadOnlyList<long>? BotUidList { get; }
    public long? LinkedChannelId { get; }
    //public bool Post { get; private set; }
    //public int? Views { get; private set; }
    public Guid CorrelationId { get; }
    public MessageSnapshot(RequestInfo request, MessageItem? messageItem, int senderMessageId, bool clearDraft,
        int groupItemCount, Dictionary<long, int> inboxItems, string? chatTitle, IReadOnlyList<long>? chatMemberUidList,
        IReadOnlyList<long>? botUidList, long? linkedChannelId,

        Guid correlationId/*, int editDate, bool pmOneSide*/)
    {
        Request = request;
        MessageItem = messageItem;
        SenderMessageId = senderMessageId;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        InboxItems = inboxItems;
        ChatTitle = chatTitle;
        ChatMemberUidList = chatMemberUidList;
        BotUidList = botUidList;
        LinkedChannelId = linkedChannelId;
        CorrelationId = correlationId;
    }
}