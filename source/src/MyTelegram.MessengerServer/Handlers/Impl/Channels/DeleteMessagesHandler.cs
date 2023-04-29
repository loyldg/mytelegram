using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Messages;
using RequestDeleteMessages = MyTelegram.Schema.Channels.RequestDeleteMessages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class DeleteMessagesHandler : RpcResultObjectHandler<RequestDeleteMessages, IAffectedMessages>,
    IDeleteMessagesHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;

    public DeleteMessagesHandler(ICommandBus commandBus,
        IPtsHelper ptsHelper)
    {
        _commandBus = commandBus;
        _ptsHelper = ptsHelper;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestDeleteMessages obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            if (obj.Id.Count > 0)
            {
                var firstMessageId = obj.Id.First();
                var command = new StartDeleteMessagesCommand(
                    MessageId.Create(inputChannel.ChannelId, firstMessageId),
                    input.ToRequestInfo(),
                    false,
                    obj.Id.ToList(),
                    ////_randomHelper.NextLong(),
                    //0,
                    //0,
                    null,
                    Guid.NewGuid());
                await _commandBus.PublishAsync(command, CancellationToken.None);
                return null!;
            }

            var pts = _ptsHelper.GetCachedPts(input.UserId);

            return new TAffectedMessages { Pts = pts, PtsCount = 0 };
        }

        throw new NotImplementedException();
    }
}
