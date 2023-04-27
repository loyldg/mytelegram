// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IMessageReactionsList : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IMessagePeerReaction> Reactions { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    string? NextOffset { get; set; }
}
