// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getOutboxReadDate" />
///</summary>
internal sealed class GetOutboxReadDateHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetOutboxReadDate, MyTelegram.Schema.IOutboxReadDate>,
    Messages.IGetOutboxReadDateHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPeerHelper _peerHelper;
    private readonly IPrivacyAppService _privacyAppService;
    public GetOutboxReadDateHandler(IQueryProcessor queryProcessor, IPeerHelper peerHelper,
        IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<MyTelegram.Schema.IOutboxReadDate> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetOutboxReadDate obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        if (peer.PeerType == PeerType.User)
        {
            var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(input.UserId));
            if (!userReadModel?.Premium ?? false)
            {
                var selfPrivacy = await _privacyAppService.GetGlobalPrivacySettingsAsync(input.UserId);
                if (selfPrivacy?.HideReadMarks ?? false)
                {
                    RpcErrors.RpcErrors403.YourPrivacyRestricted.ThrowRpcError();
                }

                var toPeerPrivacy = await _privacyAppService.GetGlobalPrivacySettingsAsync(peer.PeerId);
                if (toPeerPrivacy?.HideReadMarks ?? false)
                {
                    RpcErrors.RpcErrors403.UserPrivacyRestricted.ThrowRpcError();
                }
            }
        }

        var date = await _queryProcessor.ProcessAsync(new GetOutboxReadDateQuery(input.UserId, obj.MsgId, peer));
        var diff = CurrentDate - date;
        if (diff > MyTelegramServerDomainConsts.ChatReadMarkExpirePeriod)
        {
            RpcErrors.RpcErrors400.MsgTooOld.ThrowRpcError();
        }

        return new TOutboxReadDate
        {
            Date = date
        };
    }
}
