namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogSnapshot : ISnapshot
{
    public DialogSnapshot(
        long ownerId,
        int topMessage,
        int readInboxMaxId,
        int readOutboxMaxId,
        int unreadCount,
        Peer toPeer,
        bool unreadMark,
        bool pinned,
        int channelHistoryMinId,
        Draft? draft
    )
    {
        OwnerId = ownerId;
        TopMessage = topMessage;
        ReadInboxMaxId = readInboxMaxId;
        ReadOutboxMaxId = readOutboxMaxId;
        UnreadCount = unreadCount;
        ToPeer = toPeer;
        UnreadMark = unreadMark;
        Pinned = pinned;
        ChannelHistoryMinId = channelHistoryMinId;
        Draft = draft;
    }

    public int ChannelHistoryMinId { get; }

    public Draft? Draft { get; }

    public long OwnerId { get; }

    public bool Pinned { get; }
    public int ReadInboxMaxId { get; }
    public int ReadOutboxMaxId { get; }
    public Peer ToPeer { get; }
    public int TopMessage { get; }
    public int UnreadCount { get; }
    public bool UnreadMark { get; }
}
