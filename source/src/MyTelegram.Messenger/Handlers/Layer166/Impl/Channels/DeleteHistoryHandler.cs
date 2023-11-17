// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete the history of a <a href="https://corefork.telegram.org/api/channel">supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PARICIPANT_MISSING The current user is not in the channel.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHANNEL_TOO_BIG This channel has too many participants (&gt;1000) to be deleted.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/channels.deleteHistory" />
///</summary>
internal sealed class DeleteHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteHistory, IBool>,
    Channels.IDeleteHistoryHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ICommandBus _commandBus;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteHistoryHandler(IQueryProcessor queryProcessor,
        ICommandBus commandBus,
        IAccessHashHelper accessHashHelper)
    {
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
    }

    private async Task DeleteChannelHistoryForEveryoneAsync(long channelId, IRequestInput input)
    {
        var messageIds = (await _queryProcessor
            .ProcessAsync(new GetMessageIdListByChannelIdQuery(channelId,
                    MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize),
                default).ConfigureAwait(false)).ToList();
        // keep first message(create channel message)
        messageIds.Remove(1);
        if (messageIds.Any())
        {
            var command = new StartDeleteParticipantHistoryCommand(ChannelId.Create(channelId),
                        input.ToRequestInfo(),
                        messageIds.ToList()
                    );
            await _commandBus.PublishAsync(command, default);
        }
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            if (obj.ForEveryone)
            {
                await DeleteChannelHistoryForEveryoneAsync(inputChannel.ChannelId, input);
            }
            else
            {
                var command =
                    new ClearChannelHistoryCommand(
                        DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                        input.ToRequestInfo());
                await _commandBus.PublishAsync(command, CancellationToken.None);
            }

            return null!;
        }

        throw new NotImplementedException();
    }
}
