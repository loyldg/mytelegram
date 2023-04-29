namespace MyTelegram.MessengerServer.Extensions;

public static class Extension
{
    [return: NotNullIfNotNull("peer")]
    public static IPeer? ToPeer(this Peer? peer)
    {
        if (peer == null)
        {
            return null;
        }

        return peer.PeerType switch
        {
            PeerType.User => new TPeerUser { UserId = peer.PeerId },
            PeerType.Self => new TPeerUser { UserId = peer.PeerId },
            PeerType.Empty => new TPeerUser { UserId = peer.PeerId },
            PeerType.Chat => new TPeerChat { ChatId = peer.PeerId },
            PeerType.Channel => new TPeerChannel { ChannelId = peer.PeerId },
            _ => throw new ArgumentOutOfRangeException(
                $"peer type is invalid:peerType={peer.PeerType} peerId={peer.PeerId}")
        };
    }

    public static RequestInfo ToRequestInfo(this IRequestInput requestInput)
    {
        return new RequestInfo(requestInput.ReqMsgId,
            requestInput.UserId,
            requestInput.AuthKeyId,
            requestInput.PermAuthKeyId,
            Guid.NewGuid());
    }
}
