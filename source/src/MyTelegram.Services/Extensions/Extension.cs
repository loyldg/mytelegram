using System.Diagnostics.CodeAnalysis;
using MyTelegram.Schema;

namespace MyTelegram.Services.Extensions;

public static class Extension
{
    //public static IPeer ToPeer(this Peer? peer)
    //{
    //    if (!peer.HasValue)
    //    {
    //        return null;
    //    }

    //    return ToPeer(peer.Value);
    //}

    public static TRpcError ToRpcError(this string rpcErrorMessage, int rpcCode = 400)
    {
        return new TRpcError
        {
            ErrorCode = rpcCode,
            ErrorMessage = rpcErrorMessage
        };
    }

    public static RequestInfo ToRequestInfo(this IRequestInput requestInput)
    {
        return new RequestInfo(requestInput.ReqMsgId,
            requestInput.UserId,
            requestInput.AuthKeyId,
            requestInput.PermAuthKeyId, requestInput.RequestId, requestInput.Layer, requestInput.Date);
    }

    [return: NotNullIfNotNull("peer")]
    public static IPeer? ToPeer(this Peer? peer)
    {
        if (peer == null)
        {
            return null;
        }

        switch (peer.PeerType)
        {
            case PeerType.User:
            case PeerType.Self:
            case PeerType.Empty:
                return new TPeerUser { UserId = peer.PeerId };

            case PeerType.Chat:
                return new TPeerChat { ChatId = peer.PeerId };

            case PeerType.Channel:
                return new TPeerChannel { ChannelId = peer.PeerId };

            default:
                throw new ArgumentOutOfRangeException($"Peer type is invalid:peerType={peer.PeerType} peerId={peer.PeerId}");
        }
    }
}