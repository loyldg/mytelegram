using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ToggleSlowModeHandler : RpcResultObjectHandler<RequestToggleSlowMode, IUpdates>,
    IToggleSlowModeHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public ToggleSlowModeHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleSlowMode obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command = new ToggleSlowModeCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ReqMsgId,
                obj.Seconds,
                input.UserId);
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

            return null!;
        }

        throw new NotImplementedException();
    }
}
