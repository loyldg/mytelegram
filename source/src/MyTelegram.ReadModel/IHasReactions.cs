namespace MyTelegram.ReadModel;

public interface IHasReactions
{
    List<ReactionCount>? Reactions { get; }
    List<Reaction>? RecentReactions { get; }
    bool CanSeeList { get; }
    int MessageId { get; }
    List<UserReaction>? UserReactions { get; }

    //long SenderPeerId { get; }
}