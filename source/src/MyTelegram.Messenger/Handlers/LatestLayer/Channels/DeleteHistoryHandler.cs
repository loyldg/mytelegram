using RequestDeleteHistory = MyTelegram.Schema.Channels.RequestDeleteHistory;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

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
internal sealed class DeleteHistoryHandler(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    IAccessHashHelper accessHashHelper,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<RequestDeleteHistory, IUpdates> //, IShouldCacheRequest
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestDeleteHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            if (obj.ForEveryone)
            {
                await channelAdminRightsChecker.CheckAdminRightAsync(inputChannel.ChannelId, input.UserId,
                    admin => admin.AdminRights.DeleteMessages, RpcErrors.RpcErrors403.ChatAdminRequired);
                await DeleteChannelHistoryForEveryoneAsync(inputChannel.ChannelId, input);
            }
            else
            {
                var command =
                    new ClearChannelHistoryCommand(
                        DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                        input.ToRequestInfo(),
                        obj.MaxId
                    );
                await commandBus.PublishAsync(command);
            }

            return null!;
        }

        throw new NotImplementedException();
    }

    private async Task DeleteChannelHistoryForEveryoneAsync(long channelId, IRequestInput input)
    {
        var messageIds = (await queryProcessor
            .ProcessAsync(new GetMessageIdListByChannelIdQuery(channelId,
                    MyTelegramConsts.ClearHistoryDefaultPageSize),
                default)).ToList();
        // keep first message(create channel message)
        messageIds.Remove(1);
        if (messageIds.Count > 0)
        {
            var newTopMessageId =
                await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(channelId,
                    messageIds));
            var command =
                new StartDeleteParticipantHistoryCommand(TempId.New, input.ToRequestInfo(), channelId, messageIds,
                    newTopMessageId);
            await commandBus.PublishAsync(command);
        }
    }
}