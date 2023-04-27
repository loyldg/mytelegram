// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessagePeerReaction : IObject
{
    BitArray Flags { get; set; }
    bool Big { get; set; }
    bool Unread { get; set; }
    MyTelegram.Schema.IPeer PeerId { get; set; }
    int Date { get; set; }
    MyTelegram.Schema.IReaction Reaction { get; set; }
}
