using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditTitleHandler : RpcResultObjectHandler<RequestEditTitle, IUpdates>,
    IEditTitleHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;

    public EditTitleHandler(ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditTitle obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command = new EditChannelTitleCommand(ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                obj.Title,
                new TMessageActionChatEditTitle { Title = obj.Title }.ToBytes().ToHexString(),
                _randomHelper.NextLong(),
                Guid.NewGuid()
            );
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            return null!;
        }

        throw new NotImplementedException();
    }
}
