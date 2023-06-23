using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditChatTitleHandler : RpcResultObjectHandler<RequestEditChatTitle, IUpdates>,
    IEditChatTitleHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;

    public EditChatTitleHandler(ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditChatTitle obj)
    {
        var command = new EditChatTitleCommand(ChatId.Create(obj.ChatId),
            input.ToRequestInfo(),
            obj.Title,
            new TMessageActionChatEditTitle { Title = obj.Title }.ToBytes().ToHexString(),
            _randomHelper.NextLong(),
            Guid.NewGuid()
        );
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}