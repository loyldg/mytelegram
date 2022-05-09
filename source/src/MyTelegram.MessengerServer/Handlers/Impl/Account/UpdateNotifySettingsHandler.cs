using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Domain.Commands.PeerNotifySettings;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdateNotifySettingsHandler : RpcResultObjectHandler<RequestUpdateNotifySettings, IBool>,
    IUpdateNotifySettingsHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public UpdateNotifySettingsHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateNotifySettings obj)
    {
        if (obj.Peer is TInputNotifyPeer inputNotifyPeer)
        {
            var userId = input.UserId;
            var targetPeer = _peerHelper.GetPeer(inputNotifyPeer.Peer, userId);
            var aggregateId = PeerNotifySettingsId.Create(userId, targetPeer.PeerType, targetPeer.PeerId);
            var updatePeerNotifySettingsCommand = new UpdatePeerNotifySettingsCommand(aggregateId,
                input.ReqMsgId,
                input.UserId,
                targetPeer.PeerType,
                targetPeer.PeerId,
                obj.Settings.ShowPreviews,
                obj.Settings.Silent,
                obj.Settings.MuteUntil,
                obj.Settings.Sound
            );
            await _commandBus.PublishAsync(updatePeerNotifySettingsCommand, CancellationToken.None)
                .ConfigureAwait(false);
            return new TBoolTrue();
        }

        throw new NotImplementedException();
    }
}
