using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class TogglePreHistoryHiddenHandler : RpcResultObjectHandler<RequestTogglePreHistoryHidden, IUpdates>,
    ITogglePreHistoryHiddenHandler, IProcessedHandler //,IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public TogglePreHistoryHiddenHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestTogglePreHistoryHidden obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command = new TogglePreHistoryHiddenCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ReqMsgId,
                obj.Enabled,
                input.UserId);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}