using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetNotifySettingsHandler : RpcResultObjectHandler<RequestGetNotifySettings, IPeerNotifySettings>,
    IGetNotifySettingsHandler, IProcessedHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly IQueryProcessor _queryProcessor;

    public GetNotifySettingsHandler(IQueryProcessor queryProcessor,
        IObjectMapper objectMapper)
    {
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
    }
    
    protected override async Task<IPeerNotifySettings> HandleCoreAsync(IRequestInput input,
        RequestGetNotifySettings obj)
    {
        var userId = input.UserId;
        PeerType peerType;
        long peerId = 0;
        switch (obj.Peer)
        {
            case TInputNotifyBroadcasts:
                peerType = PeerType.Channel;
                break;

            case TInputNotifyChats:
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

                    case TInputPeerEmpty:
                    case TInputPeerSelf:
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

            case TInputNotifyUsers:
                peerType = PeerType.User;
                break;

            default:
                //throw new ArgumentOutOfRangeException();
                throw new NotSupportedException($"Not supported peer:{obj.Peer}");
        }

        var id = PeerNotifySettingsId.Create(userId, peerType, peerId);
        var peerNotifySettingsReadModel =
            await _queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(id), CancellationToken.None)
                ;
        var peerNotifySettings = peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings;

        var r = _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(peerNotifySettings);
        r.MuteUntil = 0;
        r.IosSound = new TNotificationSoundDefault();
        r.AndroidSound = new TNotificationSoundLocal
        {
            Title= "default",
            Data= "default"
        };
        r.OtherSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };
        return r;
    }
}
