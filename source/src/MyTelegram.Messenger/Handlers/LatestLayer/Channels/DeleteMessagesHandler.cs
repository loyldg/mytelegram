namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Delete messages in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler(
    ICommandBus commandBus,
    IPtsHelper ptsHelper,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteMessages,
            MyTelegram.Schema.Messages.IAffectedMessages>
{
    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteMessages obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            if (obj.Id.Count > 0)
            {
                var ids = obj.Id.ToList();

                if (!await channelAdminRightsChecker.HasChatAdminRightAsync(inputChannel.ChannelId, input.UserId,
                        p => p.AdminRights.DeleteMessages))
                {
                    var firstInboxMessageId =
                        await queryProcessor.ProcessAsync(
                            new GetFirstInboxMessageIdByMessageIdListQuery(input.UserId, inputChannel.ChannelId, ids));
                    if (firstInboxMessageId > 0)
                    {
                        RpcErrors.RpcErrors403.MessageDeleteForbidden.ThrowRpcError();
                    }
                }

                // Delete channel post message: delete all repies
                // Delete forwarded post message: update post message channelId to 777
                var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
                channelReadModel.ThrowExceptionIfChannelDeleted();

                IReadOnlyCollection<int>? repliesMessageIds = null;
                long? discussionGroupChannelId = null;
                var newTopMessageId =
                    await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(inputChannel.ChannelId, ids));
                int? newTopMessageIdForDiscussionGroup = null;

                if (channelReadModel!.Broadcast && channelReadModel.LinkedChatId.HasValue)
                {
                    discussionGroupChannelId = channelReadModel.LinkedChatId;
                    repliesMessageIds =
                      await queryProcessor.ProcessAsync(
                          new GetCommentsMessageIdListQuery(channelReadModel.ChannelId, ids));

                    if (repliesMessageIds.Count > 0)
                    {
                        newTopMessageIdForDiscussionGroup =
                          await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(channelReadModel.LinkedChatId.Value,
                              repliesMessageIds.ToList()));
                    }
                }

                var command =
                    new StartDeleteChannelMessagesCommand(TempId.New, input.ToRequestInfo(), inputChannel.ChannelId,
                        ids,
                        newTopMessageId,
                        newTopMessageIdForDiscussionGroup,
                        discussionGroupChannelId,
                        repliesMessageIds
                        );
                await commandBus.PublishAsync(command);

                return null!;
            }

            var pts = ptsHelper.GetCachedPts(input.UserId);

            return new TAffectedMessages { Pts = pts, PtsCount = 0 };
        }

        throw new NotImplementedException();
    }
}
