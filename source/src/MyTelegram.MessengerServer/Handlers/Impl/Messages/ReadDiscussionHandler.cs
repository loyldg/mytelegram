using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReadDiscussionHandler : RpcResultObjectHandler<RequestReadDiscussion, IBool>,
    IReadDiscussionHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public ReadDiscussionHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadDiscussion obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var selfDialogId = DialogId.Create(input.UserId, peer);

        var command = new ReadChannelInboxMessageCommand(
            selfDialogId,
            input.ReqMsgId,
            input.UserId,
            peer.PeerId,
            obj.ReadMaxId,
            //MessageBoxId.Create(inputChannel.ChannelId, obj.MaxId).Value,
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return new TBoolTrue();
    }
}
