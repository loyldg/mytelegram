using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Handlers.Users;
using MyTelegram.Schema.Users;
using IUserFull = MyTelegram.Schema.Users.IUserFull;

namespace MyTelegram.MessengerServer.Handlers.Impl.Users;

public class GetFullUserHandler : RpcResultObjectHandler<RequestGetFullUser, IUserFull>,
    IGetFullUserHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlUserConverter _userConverter;

    public GetFullUserHandler(IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        ITlUserConverter userConverter)
    {
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _userConverter = userConverter;
    }

    protected override async Task<IUserFull> HandleCoreAsync(IRequestInput input,
        RequestGetFullUser obj)
    {
        var userId = input.UserId;
        var targetPeer = _peerHelper.GetPeer(obj.Id, userId);
        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(targetPeer.PeerId), CancellationToken.None)
            ;
        if (user == null) ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");

        var peerSettingsId = PeerNotifySettingsId.Create(userId, targetPeer.PeerType, targetPeer.PeerId);
        var peerNotifySettings =
            await _queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(peerSettingsId),
                CancellationToken.None);
        return await _userConverter.ToUserFullAsync(user!,
            userId,
            peerNotifySettings);
    }
}