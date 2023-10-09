namespace MyTelegram.ReadModel;

public interface IDialogReadModel : IReadModel
{
    int ChannelHistoryMinId { get; }
    DateTime CreationTime { get; }

    Draft? Draft { get; }
    string Id { get; }

    int MaxSendOutMessageId { get; }
    PeerNotifySettings? NotifySettings { get; }

    long OwnerId { get; }

    //string TopMessageBoxId { get; }
    bool Pinned { get; }
    int PinnedMsgId { get; }
    int PinnedOrder { get; }
    int Pts { get; }

    int ReadInboxMaxId { get; }
    int ReadOutboxMaxId { get; }
    long ToPeerId { get; }
    PeerType ToPeerType { get; }
    int TopMessage { get; }
    int UnreadCount { get; }
    bool IsDeleted { get; }
    void SetNewTopMessageId(int topMessageId);
    int? TtlPeriod { get; }
    int UnreadMentionsCount { get; }
    int UnreadReactionsCount { get; }
}