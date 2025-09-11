namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Join a channel/supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_INVALID Invalid chat.
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// 400 INVITE_REQUEST_SENT You have successfully requested to join this chat or channel.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// See <a href="https://corefork.telegram.org/method/channels.joinChannel" />
///</summary>
internal sealed class JoinChannelHandler(
    ICommandBus commandBus,
    IChannelAppService channelAppService,
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<RequestJoinChannel, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestJoinChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);
            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
            channelReadModel.ThrowExceptionIfChannelDeleted();

            var channelMemberReadModel =
                await queryProcessor.ProcessAsync(
                    new GetChannelMemberByUserIdQuery(channelReadModel.ChannelId, input.UserId));

            if (channelMemberReadModel != null)
            {
                if (channelMemberReadModel.Kicked)
                {
                    RpcErrors.RpcErrors400.ChannelPrivate.ThrowRpcError();
                }
                else
                {
                    if (!channelMemberReadModel.Left)
                    {
                        RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
                    }
                }
            }

            var channelHistoryMinId = 0;
            if (channelReadModel.HiddenPreHistory)
            {
                channelHistoryMinId = channelReadModel.TopMessageId;
            }

            if (channelReadModel.JoinRequest)
            {
                var command = new CreateJoinChannelRequestCommand(
                    JoinChannelId.Create(channelReadModel.ChannelId, input.UserId), input.ToRequestInfo(),
                    channelReadModel.ChannelId, null);
                await commandBus.PublishAsync(command);
            }
            else
            {
                var command = new StartJoinChannelCommand(TempId.New, input.ToRequestInfo(), channelReadModel.ChannelId,
                    channelReadModel.Broadcast, channelReadModel.TopMessageId, channelHistoryMinId);
                await commandBus.PublishAsync(command);
            }


            return null!;
        }

        throw new NotImplementedException();
    }
}
