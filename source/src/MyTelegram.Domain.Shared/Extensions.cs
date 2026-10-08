// ReSharper disable once CheckNamespace

using System.Collections;

namespace MyTelegram;

public static class Extensions
{
    public static bool IsUserPeer(this long peerId)
    {
        return peerId is >= MyTelegramConsts.UserIdBase and < MyTelegramConsts.ChatIdBase;
    }

    public static bool IsChannelPeer(this long peerId)
    {
        return peerId > MyTelegramConsts.ChannelIdBase;
    }

    public static bool IsBotPeer(this long peerId)
    {
        return peerId is >= MyTelegramConsts.BotUserIdBase and < MyTelegramConsts.ChatIdBase;
    }

    public static Peer ToUserPeer(this long peerId)
    {
        return new Peer(PeerType.User, peerId);
    }

    public static Peer ToChatPeer(this long peerId)
    {
        return new Peer(PeerType.Chat, peerId);
    }

    public static Peer ToPeer(this long peerId)
    {
        var peerType = peerId switch
        {
            < MyTelegramConsts.ChatIdBase => PeerType.User,
            >= MyTelegramConsts.ChatIdBase and < MyTelegramConsts.ChannelIdBase => PeerType.Chat,
            >= MyTelegramConsts.ChannelIdBase => PeerType.Channel
        };

        return new Peer(peerType, peerId);
    }

    public static Peer ToChannelPeer(this long peerId)
    {
        return new Peer(PeerType.Channel, peerId);
    }

    public static Peer ToUserPeer(this int peerId)
    {
        return new Peer(PeerType.User, peerId);
    }

    public static Peer ToChatPeer(this int peerId)
    {
        return new Peer(PeerType.Chat, peerId);
    }

    public static Peer ToChannelPeer(this int peerId)
    {
        return new Peer(PeerType.Channel, peerId);
    }

    public static int ToInt32(this BitArray bitArray)
    {
        return BitConverter.ToInt32(ToByteArray(bitArray));
    }

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var bytes = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    public static BitArray ToBitArray(this int value)
    {
        return new BitArray(BitConverter.GetBytes(value));
    }
}