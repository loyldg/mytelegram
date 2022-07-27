using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ForwardMessagesHandler : RpcResultObjectHandler<RequestForwardMessages, IUpdates>,
    IForwardMessagesHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public ForwardMessagesHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestForwardMessages obj)
    {
        var fromPeer = _peerHelper.GetPeer(obj.FromPeer, input.UserId);
        var toPeer = _peerHelper.GetPeer(obj.ToPeer, input.UserId);
        var firstId = obj.Id.First();
        var ownerPeerId = fromPeer.PeerType == PeerType.Channel ? fromPeer.PeerId : input.UserId;
        var command = new StartForwardMessageCommand(MessageId.Create(ownerPeerId, firstId),
            input.ToRequestInfo(),
            fromPeer,
            toPeer,
            obj.Id.ToList(),
            obj.RandomId.ToList(),
            false,
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
        return null!;
    }
}
