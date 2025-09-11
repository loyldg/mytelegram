namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Delete all messages sent by a specific participant of a given supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.deleteParticipantHistory" />
///</summary>
internal sealed class DeleteParticipantHistoryHandler(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IPtsHelper ptsHelper,
    IAccessHashHelper accessHashHelper,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<RequestDeleteParticipantHistory,
            IAffectedHistory>
{
    protected override async Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestDeleteParticipantHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            await channelAdminRightsChecker.CheckAdminRightAsync(inputChannel.ChannelId, input.UserId,
                rights => rights.AdminRights.DeleteMessages, RpcErrors.RpcErrors403.ChatAdminRequired);

            var peer = peerHelper.GetPeer(obj.Participant);
            var messageIds = (await queryProcessor
                .ProcessAsync(new GetMessageIdListByUserIdQuery(inputChannel.ChannelId,
                    peer.PeerId,
                    MyTelegramConsts.ClearHistoryDefaultPageSize))).ToList();

            if (messageIds.Count > 0)
            {
                var newTopMessageId =
                    await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(inputChannel.ChannelId,
                        messageIds));

                var command = new StartDeleteParticipantHistoryCommand(TempId.New,
                    input.ToRequestInfo(),
                    inputChannel.ChannelId,
                    messageIds,
                    newTopMessageId
                );
                await commandBus.PublishAsync(command);

                return null!;
            }

            return new TAffectedHistory
            {
                Pts = ptsHelper.GetCachedPts(inputChannel.ChannelId),
                PtsCount = 0,
                Offset = 0
            };
        }

        throw new NotImplementedException();
    }
}
