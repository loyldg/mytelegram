using MyTelegram.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Extensions;

public static class Extension
{
    public static Peer ToChannelPeer(this IInputChannel channel)
    {
        if (channel is TInputChannel inputChannel)
        {
            return new Peer(PeerType.Channel, inputChannel.ChannelId, inputChannel.AccessHash);
        }

        RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        return null!;
    }

    public static IMessageReplyHeader? ToMessageReplyHeader(this IInputReplyTo? inputReplyTo)
    {
        return inputReplyTo switch
        {
            TInputReplyToMessage inputReplyToMessage => new TMessageReplyHeader
            {
                ForumTopic = inputReplyToMessage.TopMsgId.HasValue,
                Quote = !string.IsNullOrEmpty(inputReplyToMessage.QuoteText),
                QuoteEntities = inputReplyToMessage.QuoteEntities,
                QuoteText = inputReplyToMessage.QuoteText,
                ReplyToMsgId = inputReplyToMessage.ReplyToMsgId,
                ReplyToTopId = inputReplyToMessage.TopMsgId,
                ReplyToPeerId = inputReplyToMessage.ReplyToPeerId?.ToPeer().ToPeer(),
            },
            TInputReplyToStory inputReplyToStory => new TMessageReplyStoryHeader
            {
                StoryId = inputReplyToStory.StoryId,
                Peer = inputReplyToStory.Peer.ToPeer().ToPeer()
            },
            _ => null
        };
    }

    public static Peer ToPeer(this IInputPeer peer,
        long selfUserId = 0)
    {
        PeerType peerType;
        long peerId;
        long accessHash = 0;

        switch (peer)
        {
            case TInputPeerChannel inputPeerChannel:
                peerType = PeerType.Channel;
                peerId = inputPeerChannel.ChannelId;
                accessHash = inputPeerChannel.AccessHash;
                break;
                //case TInputPeerChannelFromMessage inputPeerChannelFromMessage:
                //    peerType = PeerType.Channel;
                //    peerId = inputPeerChannelFromMessage.ChannelId;

                break;
            case TInputPeerChat inputPeerChat:
                peerType = PeerType.Chat;
                peerId = inputPeerChat.ChatId;
                break;
            case TInputPeerEmpty:
                peerType = PeerType.Empty;
                peerId = selfUserId;
                break;
            case TInputPeerSelf _:
                peerType = PeerType.Self;
                peerId = selfUserId;
                break;
            case TInputPeerUser inputPeerUser:
                peerType = PeerType.User;
                peerId = inputPeerUser.UserId;
                accessHash = inputPeerUser.AccessHash;
                break;
            //case TInputPeerUserFromMessage inputPeerUserFromMessage:
            //break;
            default:
                throw new NotSupportedException(peer.GetType().Name);
        }

        if (peerType == PeerType.User && peerId == selfUserId)
        {
            peerType = PeerType.Self;
        }

        return new Peer(peerType, peerId, accessHash);
    }

    public static Peer ToPeer(this IInputUser userPeer,
        long selfUserId)
    {
        var peerId = 0L;
        var peerType = PeerType.User;
        var accessHash = 0L;
        switch (userPeer)
        {
            case TInputUser inputUser:
                peerId = inputUser.UserId;
                accessHash = inputUser.AccessHash;
                break;
            case TInputUserEmpty:
                break;
            case TInputUserFromMessage inputUserFromMessage:
                peerId = inputUserFromMessage.UserId;

                break;
            case TInputUserSelf:
                peerType = PeerType.Self;
                peerId = selfUserId;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(userPeer));
        }

        return new Peer(peerType, peerId, accessHash);
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

    public static IPeerColor? ToPeerColor(this PeerColor? peerColor)
    {
        if (peerColor == null || (!peerColor.Color.HasValue && !peerColor.BackgroundEmojiId.HasValue))
        {
            return default;
        }

        return new TPeerColor
        {
            Color = peerColor.Color,
            BackgroundEmojiId = peerColor.BackgroundEmojiId
        };
    }
    public static RequestInfo ToRequestInfo(this IRequestInput requestInput)
    {
        return new RequestInfo(requestInput.ReqMsgId,
            requestInput.UserId,
            requestInput.AuthKeyId,
            requestInput.PermAuthKeyId, requestInput.RequestId, requestInput.Layer, requestInput.Date);
    }

    public static TRpcError ToRpcError(this string rpcErrorMessage, int rpcCode = 400)
    {
        return new TRpcError
        {
            ErrorCode = rpcCode,
            ErrorMessage = rpcErrorMessage
        };
    }
}