// ReSharper disable once CheckNamespace
namespace MyTelegram;

public static class Extensions
{
    public static Peer ToUserPeer(this long peerId) => new(PeerType.User, peerId);
    public static Peer ToChatPeer(this long peerId) => new(PeerType.Chat, peerId);
    public static Peer ToChannelPeer(this long peerId) => new(PeerType.Channel, peerId);

    public static Peer ToUserPeer(this int peerId) => new(PeerType.User, peerId);
    public static Peer ToChatPeer(this int peerId) => new(PeerType.Chat, peerId);
    public static Peer ToChannelPeer(this int peerId) => new(PeerType.Channel, peerId);
}
