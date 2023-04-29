namespace MyTelegram.ReadModel;

public interface IReplyReadModel : IReadModel
{
    int Replies { get; }
    int RepliesPts { get; }
    long SavedFromPeerId { get; }
    int SavedFromMsgId { get; }
    long ChannelId { get; }
    int ReplyToMsgId { get; }
    int MaxId { get; }

    IReadOnlyCollection<Peer>? RecentRepliers { get; }
    //int ReadMaxId { get; }
}
