using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPeerSettingsHandler : RpcResultObjectHandler<RequestGetPeerSettings, MyTelegram.Schema.Messages.IPeerSettings>,
    IGetPeerSettingsHandler, IProcessedHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly IPeerHelper _peerHelper;
    private readonly IPeerSettingsAppService _peerSettingsAppService;

    public GetPeerSettingsHandler(IPeerSettingsAppService peerSettingsAppService,
        IPeerHelper peerHelper,
        IObjectMapper objectMapper)
    {
        _peerSettingsAppService = peerSettingsAppService;
        _peerHelper = peerHelper;
        _objectMapper = objectMapper;
    }

    protected override async Task<MyTelegram.Schema.Messages.IPeerSettings> HandleCoreAsync(IRequestInput input,
        RequestGetPeerSettings obj)
    {
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);
        var r = await _peerSettingsAppService.GetAsync(userId, peer);

        var peerSettings = new MyTelegram.Schema.Messages.TPeerSettings
        {
            Chats=new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Settings = _objectMapper.Map<PeerSettings, MyTelegram.Schema.TPeerSettings>(r)
        };
        return peerSettings;
    }
}
