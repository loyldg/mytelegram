using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class DeleteHistoryHandler : RpcResultObjectHandler<RequestDeleteHistory, IBool>,
    IDeleteHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public DeleteHistoryHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command =
                new ClearChannelHistoryCommand(DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                    input.ReqMsgId);
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            return null!;
        }

        throw new NotImplementedException();
    }
}
