using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class CreateChannelHandler : RpcResultObjectHandler<RequestCreateChannel, IUpdates>,
    ICreateChannelHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IIdGenerator _idGenerator;
    private readonly IRandomHelper _randomHelper;

    public CreateChannelHandler(ICommandBus commandBus,
        IIdGenerator idGenerator,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _idGenerator = idGenerator;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestCreateChannel obj)
    {
        var channelId = await _idGenerator.NextLongIdAsync(IdType.ChannelId).ConfigureAwait(false);
        var accessHash = _randomHelper.NextLong();
        var date = DateTime.UtcNow.ToTimestamp();
        var command = new CreateChannelCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            channelId,
            input.UserId,
            obj.Title,
            obj.Broadcast,
            obj.Megagroup,
            obj.About ?? string.Empty,
            obj.Address,
            accessHash,
            date,
            _randomHelper.NextLong(),
            new TMessageActionChannelCreate { Title = obj.Title }.ToBytes().ToHexString(),
            Guid.NewGuid()
        );
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        // send message services to channel
        return null!;
    }
}
