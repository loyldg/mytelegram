using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteChatHandler : RpcResultObjectHandler<RequestDeleteChat, IBool>,
    IDeleteChatHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;

    public DeleteChatHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteChat obj)
    {
        var command = new DeleteChatCommand(ChatId.Create(obj.ChatId), input.ToRequestInfo(), Guid.NewGuid());
        await _commandBus.PublishAsync(command, default);

        return new TBoolTrue();
    }
}
