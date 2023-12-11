// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Gets current notification settings for a given user/group, from all users/all groups.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getNotifySettings" />
///</summary>
internal sealed class GetNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetNotifySettings, MyTelegram.Schema.IPeerNotifySettings>,
    Account.IGetNotifySettingsHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly IQueryProcessor _queryProcessor;

    public GetNotifySettingsHandler(IQueryProcessor queryProcessor,
        IObjectMapper objectMapper)
    {
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
    }

    //private readonly ipeerno
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
            await _queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(id), CancellationToken.None)
         ;
        var peerNotifySettings = peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings;

        var r = _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(peerNotifySettings);
        r.MuteUntil = 0;
        r.IosSound = new TNotificationSoundDefault();

        r.AndroidSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };
        r.OtherSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };

        return r;
    }
}
