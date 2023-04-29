// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageReactions : IObject
{
    BitArray Flags { get; set; }
    bool Min { get; set; }
    bool CanSeeList { get; set; }
    TVector<Schema.IReactionCount> Results { get; set; }
    TVector<Schema.IMessagePeerReaction>? RecentReactions { get; set; }
}
