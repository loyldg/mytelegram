namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageSnapshot : ISnapshot
{
    public MessageSnapshot(MessageItem messageItem,
        List<InboxItem> inboxItems,
        int senderMessageId,
        bool pinned,
        int editDate,
        //bool editHide,
        bool edited,
        int pts,
        List<Peer> recentRepliers)
    {
        MessageItem = messageItem;
        InboxItems = inboxItems;
        SenderMessageId = senderMessageId;
        Pinned = pinned;
        EditDate = editDate;
        //EditHide = editHide;
        Edited = edited;
        Pts = pts;
        RecentRepliers = recentRepliers;
    }

    public int EditDate { get; }

    //public bool EditHide { get; }
    public bool Edited { get; }

    public List<InboxItem> InboxItems { get; }
    public MessageItem MessageItem { get; }
    public bool Pinned { get; }
    public int Pts { get; }
    public List<Peer> RecentRepliers { get; }
    public int SenderMessageId { get; }
}
