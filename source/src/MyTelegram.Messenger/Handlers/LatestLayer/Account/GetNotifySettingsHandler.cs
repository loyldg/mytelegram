namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Gets current notification settings for a given user/group, from all users/all groups.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getNotifySettings" />
///</summary>
internal sealed class GetNotifySettingsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IPeerNotifySettingsConverter> layeredService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetNotifySettings, MyTelegram.Schema.IPeerNotifySettings>
{
    protected override async Task<MyTelegram.Schema.IPeerNotifySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetNotifySettings obj)
    {
        var userId = input.UserId;
        PeerNotifySettingsId id;
        var peerType = PeerType.Unknown;
        long peerId = 0;
        switch (obj.Peer)
        {
            case TInputNotifyForumTopic inputNotifyForumTopic:
                peerType = PeerType.Channel;
                break;
            case TInputNotifyBroadcasts inputNotifyBroadcasts:
                peerType = PeerType.Channel;
                break;

            case TInputNotifyChats inputNotifyChats:
                peerType = PeerType.Chat;
                break;

            case TInputNotifyPeer inputNotifyPeer:
                switch (inputNotifyPeer.Peer)
                {
                    case TInputPeerChannel inputPeerChannel:
                        peerType = PeerType.Channel;
                        peerId = inputPeerChannel.ChannelId;
                        break;

                    case TInputPeerChat inputPeerChat:
                        peerType = PeerType.Chat;
                        peerId = inputPeerChat.ChatId;
                        break;

                    case TInputPeerEmpty _:
                    case TInputPeerSelf _:
                        peerType = PeerType.User;
                        peerId = userId;
                        break;

                    case TInputPeerUser inputPeerUser:
                        peerType = PeerType.User;
                        peerId = inputPeerUser.UserId;
                        break;

                    default:
                        throw new NotSupportedException();
                }

                break;

            case TInputNotifyUsers inputNotifyUsers:
                peerType = PeerType.User;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        id = PeerNotifySettingsId.Create(userId, peerType, peerId);
        var peerNotifySettingsReadModel =
            await queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(id.Value), CancellationToken.None);

        return layeredService.GetConverter(input.Layer).ToPeerNotifySettings(peerNotifySettingsReadModel);
    }
}
