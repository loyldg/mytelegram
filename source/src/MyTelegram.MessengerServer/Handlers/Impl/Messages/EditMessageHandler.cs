using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditMessageHandler : RpcResultObjectHandler<RequestEditMessage, IUpdates>,
    IEditMessageHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IPeerHelper _peerHelper;

    public EditMessageHandler(IMediaHelper mediaHelper,
        ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _mediaHelper = mediaHelper;
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditMessage obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var ownerPeerId = input.UserId;
        if (peer.PeerType == PeerType.Channel)
        {
            ownerPeerId = peer.PeerId;
        }

        byte[]? mediaBytes = null;
        if (obj.Media != null)
        {
            var media = await _mediaHelper.SaveMediaAsync(obj.Media).ConfigureAwait(false);
            mediaBytes = media.ToBytes();
        }

        var command = new EditOutboxMessageCommand(MessageId.Create(ownerPeerId, obj.Id),
            input.ToRequestInfo(),
            obj.Id,
            obj.Message ?? string.Empty,
            obj.Entities.ToBytes(),
            CurrentDate,
            mediaBytes,
            Guid.NewGuid()
        );
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return null!;
        //throw new NotImplementedException();
    }
}
