using MyTelegram.Schema;
using MyTelegram.Services.Exceptions;

namespace MyTelegram.Services.Services;

public class PeerHelper : IPeerHelper //, ISingletonDependency
{
    //public const long UserIdInitId =  6000000;
    //public const long ChatIdInitId =  50000000000;
    //public const long ChannelInitId = 800000000000;
    //public const long BotUserInitId = 90000000000000;

    public Peer GetChannel(IInputChannel channel)
    {
        if (channel is TInputChannel inputChannel)
        {
            return new Peer(PeerType.Channel, inputChannel.ChannelId, inputChannel.AccessHash);
        }

        throw new BadRequestException("CHANNEL_INVALID");
        //ThrowHelper.ThrowUserFriendlyException("CHANNEL_INVALID");
    }

    public Peer GetPeer(IInputPeer peer,
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

    public Peer GetPeer(IInputUser userPeer,
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

    public bool IsChannelPeer(long peerId)
    {
        return peerId is >= MyTelegramServerDomainConsts.ChannelInitId and < MyTelegramServerDomainConsts.BotUserInitId;
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

    public Peer GetPeer(long peerId)
    {
        var peerType = GetPeerType(peerId);
        return new Peer(peerType, peerId);
    }

    public bool IsEncryptedDialogPeer(long dialogId)
    {
        //-9223372036854775808=ulong 0x8000000000000000L
        return (dialogId & 0x4000000000000000L) != 0 && (dialogId & -9223372036854775808) == 0;
    }
}