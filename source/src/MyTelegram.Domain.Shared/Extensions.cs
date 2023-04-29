// ReSharper disable once CheckNamespace

namespace MyTelegram;

public static class Extensions
{
    public static Peer ToChannelPeer(this long peerId)
    {
        return new Peer(PeerType.Channel, peerId);
    }

    public static Peer ToChannelPeer(this int peerId)
    {
        return new Peer(PeerType.Channel, peerId);
    }

    public static Peer ToChatPeer(this long peerId)
    {
        return new Peer(PeerType.Chat, peerId);
    }

    public static Peer ToChatPeer(this int peerId)
    {
        return new Peer(PeerType.Chat, peerId);
    }

    public static Peer ToUserPeer(this long peerId)
    {
        return new Peer(PeerType.User, peerId);
    }

    public static Peer ToUserPeer(this int peerId)
    {
        return new Peer(PeerType.User, peerId);
    }
}
