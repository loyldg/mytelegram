using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public interface IPeerHelper
{
    Peer GetChannel(IInputChannel channel);

    Peer GetPeer(IInputPeer peer,
        long selfUserId = 0);

    Peer GetPeer(IInputUser userPeer,
        long selfUserId = 0);

    PeerType GetPeerType(long peerId);

    bool IsBotUser(long userId);
    bool IsChannelPeer(long peerId);
    IPeer ToPeer(Peer peer);

    IPeer ToPeer(PeerType peerType,
        long peerId);

    Peer GetPeer(long peerId);

    bool IsEncryptedDialogPeer(long peerId);
}