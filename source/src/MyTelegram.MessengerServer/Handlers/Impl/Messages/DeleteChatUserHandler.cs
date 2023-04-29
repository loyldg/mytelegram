using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteChatUserHandler : RpcResultObjectHandler<RequestDeleteChatUser, IUpdates>,
    IDeleteChatUserHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public DeleteChatUserHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteChatUser obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId, input.UserId);
        var command = new DeleteChatUserCommand(ChatId.Create(obj.ChatId),
            input.ToRequestInfo(),
            peer.PeerId,
            new TMessageActionChatDeleteUser { UserId = peer.PeerId }.ToBytes().ToHexString(),
            _randomHelper.NextLong(),
            Guid.NewGuid()
        );
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
