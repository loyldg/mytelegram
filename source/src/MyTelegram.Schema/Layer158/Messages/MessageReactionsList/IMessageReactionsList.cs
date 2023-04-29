// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IMessageReactionsList : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<Schema.IMessagePeerReaction> Reactions { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
    string? NextOffset { get; set; }
}
