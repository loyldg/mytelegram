// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Create a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_LOCATED_TOO_MUCH The user has reached the limit of public geogroups.
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 500 CHANNEL_ID_GENERATE_FAILED &nbsp;
/// 400 CHAT_ABOUT_TOO_LONG Chat about too long.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// 406 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/channels.createChannel" />
///</summary>
internal sealed class CreateChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCreateChannel, MyTelegram.Schema.IUpdates>,
    Channels.ICreateChannelHandler
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
        var channelId = await _idGenerator.NextLongIdAsync(IdType.ChannelId);
        var accessHash = _randomHelper.NextLong();
        var date = DateTime.UtcNow.ToTimestamp();

        var megagroup = obj.Megagroup;
        if (!obj.Broadcast)
        {
            megagroup = true;
        }

        var command = new CreateChannelCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            channelId,
            input.UserId,
            obj.Title,
            obj.Broadcast,
            megagroup,
            obj.About ?? string.Empty,
            obj.Address,
            accessHash,
            date,
            _randomHelper.NextLong(),
            new TMessageActionChannelCreate { Title = obj.Title }.ToBytes().ToHexString(),
            null,
            false,
            null,
            null,
            null
        );
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
