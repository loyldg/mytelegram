using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UpdatePinnedMessageHandler : RpcResultObjectHandler<RequestUpdatePinnedMessage, IUpdates>,
    IUpdatePinnedMessageHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public UpdatePinnedMessageHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestUpdatePinnedMessage obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : input.UserId;
        var command = new StartUpdatePinnedMessageCommand(MessageId.Create(ownerPeerId, obj.Id),
            input.ToRequestInfo(),
            !obj.Unpin,
            obj.Silent,
            obj.PmOneside,
            CurrentDate,
            _randomHelper.NextLong(),
            new TMessageActionPinMessage().ToBytes().ToHexString(),
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}