// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IVotesList : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IMessageUserVote> Votes { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    string? NextOffset { get; set; }
}
