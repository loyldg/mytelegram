// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessagePeerReaction : IObject
{
    BitArray Flags { get; set; }
    bool Big { get; set; }
    bool Unread { get; set; }
    Schema.IPeer PeerId { get; set; }
    int Date { get; set; }
    Schema.IReaction Reaction { get; set; }
}
