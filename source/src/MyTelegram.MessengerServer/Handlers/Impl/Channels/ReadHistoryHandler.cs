using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ReadHistoryHandler : RpcResultObjectHandler<RequestReadHistory, IBool>,
    IReadHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public ReadHistoryHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command = new ReadChannelInboxMessageCommand(
                DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                input.ReqMsgId,
                input.UserId,
                inputChannel.ChannelId,
                obj.MaxId,
                //MessageBoxId.Create(inputChannel.ChannelId, obj.MaxId).Value,
                Guid.NewGuid());
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotSupportedException("not supported peer,read channel history only support channel");
    }
}
