using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class DeleteHistoryHandler : RpcResultObjectHandler<RequestDeleteHistory, IBool>,
    IDeleteHistoryHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ICommandBus _commandBus;

    public DeleteHistoryHandler(IQueryProcessor queryProcessor,
        ICommandBus commandBus)
    {
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
    }

    private async Task DeleteChannelHistoryForEveryoneAsync(long channelId, IRequestInput input)
    {
        var messageIds = (await _queryProcessor
            .ProcessAsync(new GetMessageIdListByChannelIdQuery(channelId,
                    MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize),
                default).ConfigureAwait(false)).ToList();
        messageIds.Remove(1);
        if (messageIds.Any())
        {
            var command = new StartDeleteParticipantHistoryCommand(ChannelId.Create(channelId),
                        input.ToRequestInfo(),
                        messageIds.ToList(),
                        Guid.NewGuid()
                    );
            await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
        }
    }
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            if (obj.ForEveryone)
            {
                await DeleteChannelHistoryForEveryoneAsync(inputChannel.ChannelId, input).ConfigureAwait(false);
            }
            else
            {
            var command =
                    new ClearChannelHistoryCommand(
                        DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                    input.ReqMsgId);
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            }
            return null!;
        }

        throw new NotImplementedException();
    }
}
