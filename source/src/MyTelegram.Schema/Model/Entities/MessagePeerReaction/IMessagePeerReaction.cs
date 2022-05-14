// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessagePeerReaction : IObject
{
    BitArray Flags { get; set; }
    bool Big { get; set; }
    bool Unread { get; set; }
    MyTelegram.Schema.IPeer PeerId { get; set; }
    string Reaction { get; set; }

}
