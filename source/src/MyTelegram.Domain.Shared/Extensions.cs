// ReSharper disable once CheckNamespace
using System.Collections;

namespace MyTelegram;

public static class Extensions
{
    public static Peer ToUserPeer(this long peerId) => new(PeerType.User, peerId);
    public static Peer ToChatPeer(this long peerId) => new(PeerType.Chat, peerId);
    public static Peer ToChannelPeer(this long peerId) => new(PeerType.Channel, peerId);

    public static Peer ToUserPeer(this int peerId) => new(PeerType.User, peerId);
    public static Peer ToChatPeer(this int peerId) => new(PeerType.Chat, peerId);
    public static Peer ToChannelPeer(this int peerId) => new(PeerType.Channel, peerId);

    public static int ToInt(this BitArray bitArray)
    {
        return BitConverter.ToInt32(ToByteArray(bitArray));
    }

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var bytes = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }
}
