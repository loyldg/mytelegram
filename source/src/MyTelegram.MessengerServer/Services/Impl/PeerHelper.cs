namespace MyTelegram.MessengerServer.Services.Impl;

public class PeerHelper : IPeerHelper
{
    public Peer GetChannel(IInputChannel channel)
    {
        if (channel is TInputChannel inputChannel)
        {
            return new Peer(PeerType.Channel, inputChannel.ChannelId);
        }

        throw new BadRequestException(RpcErrorMessages.ChannelInvalid);
    }

    public Peer GetPeer(IInputPeer peer,
        long selfUserId = 0)
    {
        PeerType peerType;
        long peerId;

        switch (peer)
        {
            case TInputPeerChannel inputPeerChannel:
                peerType = PeerType.Channel;
                peerId = inputPeerChannel.ChannelId;
                break;
            case TInputPeerChat inputPeerChat:
                peerType = PeerType.Chat;
                peerId = inputPeerChat.ChatId;
                break;
            case TInputPeerEmpty:
                peerType = PeerType.Empty;
                peerId = selfUserId;
                break;
            case TInputPeerSelf:
                peerType = PeerType.Self;
                peerId = selfUserId;
                break;
            case TInputPeerUser inputPeerUser:
                peerType = PeerType.User;
                peerId = inputPeerUser.UserId;
                break;
            default:
                throw new NotSupportedException(peer.GetType().Name);
        }

        if (peerType == PeerType.User && peerId == selfUserId)
        {
            peerType = PeerType.Self;
        }

        return new Peer(peerType, peerId);
    }

    public Peer GetPeer(IInputUser userPeer,
        long selfUserId)
    {
        var peerId = 0L;
        var peerType = PeerType.User;
        switch (userPeer)
        {
            case TInputUser inputUser:
                peerId = inputUser.UserId;
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

        return new Peer(peerType, peerId);
    }

    public IPeer ToPeer(Peer peer)
    {
        return ToPeer(peer.PeerType, peer.PeerId);
    }

    public IPeer ToPeer(PeerType peerType,
        long peerId)
    {
        return peerType switch
        {
            PeerType.Self => new TPeerUser { UserId = peerId },
            PeerType.User => new TPeerUser { UserId = peerId },
            PeerType.Chat => new TPeerChat { ChatId = peerId },
            PeerType.Channel => new TPeerChannel { ChannelId = peerId },
            _ => throw new ArgumentOutOfRangeException(nameof(peerType),
                $"Peer({peerType} {peerId}) can not convert to IPeer")
        };
    }

    public bool IsBotUser(long userId)
    {
        return userId >= MyTelegramServerDomainConsts.BotUserInitId;
    }

    public PeerType GetPeerType(long peerId)
    {
        var peerType = peerId switch
        {
            < MyTelegramServerDomainConsts.ChatIdInitId => PeerType.User,
            >= MyTelegramServerDomainConsts.ChatIdInitId and < MyTelegramServerDomainConsts.ChannelInitId => PeerType
                .Chat,
            >= MyTelegramServerDomainConsts.ChannelInitId and < MyTelegramServerDomainConsts.BotUserInitId =>
                PeerType.Channel,
            >= MyTelegramServerDomainConsts.BotUserInitId => PeerType.User
        };

        return peerType;
    }
}
