using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class AddChatUserHandler : RpcResultObjectHandler<RequestAddChatUser, IUpdates>,
    IAddChatUserHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public AddChatUserHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAddChatUser obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId);
        var command = new AddChatUserCommand(ChatId.Create(obj.ChatId),
            input.ToRequestInfo(),
            peer.PeerId,
            CurrentDate,
            new TMessageActionChatAddUser { Users = new TVector<long>(peer.PeerId) }.ToBytes().ToHexString(),
            _randomHelper.NextLong(),
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
        return null!;
    }
}
