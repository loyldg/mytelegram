namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#channels">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getFullChannel" />
///</summary>
internal sealed class GetFullChannelHandler(
    IQueryProcessor queryProcessor,
    //ILayeredService<IChatConverter> layeredService,
    IUserConverterService userConverterService,
    IChatConverterService chatConverterService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService,
    ILogger<GetFullChannelHandler> logger,
    IChannelAppService channelAppService,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<RequestGetFullChannel, MyTelegram.Schema.Messages.IChatFull>
{
    protected override async Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetFullChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channelId = inputChannel.ChannelId;
            await accessHashHelper.CheckAccessHashAsync(input, channelId, inputChannel.AccessHash, AccessHashType.Channel);
            var channelReadModel = await channelAppService.GetAsync(channelId);
            if (channelReadModel == null!)
            {
                RpcErrors.RpcErrors400.ChannelInvalid.ThrowRpcError();
            }

            var channelFullReadModel = await channelAppService.GetChannelFullAsync(channelId);
            if (channelFullReadModel == null)
            {
                RpcErrors.RpcErrors400.ChannelInvalid.ThrowRpcError();
            }

            if (await channelAppService.SendRpcErrorIfNotChannelMemberAsync(input, channelReadModel!))
            {
                return null!;
            }

            var dialogReadModel = await queryProcessor.ProcessAsync(
                new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, channelId).Value));
            if (dialogReadModel == null)
            {
                logger.LogWarning("Dialog not exists, userId: {UserId}, toPeer: {ToPeer}", input.UserId, new Peer(PeerType.Channel, channelId));
            }
            else
            {
                channelFullReadModel!.ReadInboxMaxId = dialogReadModel.ReadInboxMaxId;
                channelFullReadModel.ReadOutboxMaxId = dialogReadModel.ReadOutboxMaxId;
                var maxId = new[]{dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId,
                    dialogReadModel.ChannelHistoryMinId}.Max();
                channelFullReadModel.UnreadCount = channelReadModel!.TopMessageId - maxId;
            }

            var channelMemberReadModel = await queryProcessor
                .ProcessAsync(new GetChannelMemberByUserIdQuery(channelId, input.UserId));

            var peerNotifySettings = await queryProcessor
                .ProcessAsync(
                    new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                        PeerType.Channel,
                        channelId).Value));
            var photoReadModel = await photoAppService.GetAsync(channelReadModel!.PhotoId);
            IChatInviteReadModel? chatInviteReadModel = null;
            if (channelReadModel.AdminList.Any(p => p.UserId == input.UserId))
            {
                chatInviteReadModel =
                    await queryProcessor.ProcessAsync(new GetPermanentChatInviteQuery(channelId));
            }

            var chatFull = chatConverterService.ToChannelFull(
                input,
                channelReadModel,
                photoReadModel,
                channelFullReadModel!,
                channelMemberReadModel,
                peerNotifySettings,
                chatInviteReadModel,
                input.Layer
                );

            var fullChat = chatFull.FullChat;
            if (fullChat is ILayeredChannelFull layeredChannelFull)
            {
                layeredChannelFull.ViewForumAsMessages = dialogReadModel?.ViewForumAsMessages ?? false;
                layeredChannelFull.ParticipantsHidden = channelReadModel.ParticipantsHidden;
                if (channelMemberReadModel == null || channelMemberReadModel.Left || channelMemberReadModel.Kicked)
                {
                    layeredChannelFull.CanViewParticipants = false;
                }

                // Set pending requests for channel admin
                await SetRecentRequestersAsync(input, layeredChannelFull, chatFull);
            }
            IChat? linkedChannel = null;

            if (channelFullReadModel!.LinkedChatId.HasValue)
            {
                var linkedChannelReadModel =
                    await channelAppService.GetAsync(channelFullReadModel.LinkedChatId.Value);
                if (linkedChannelReadModel != null!)
                {
                    var linkedChannelPhotoReadModel = await photoAppService.GetAsync(linkedChannelReadModel.PhotoId);
                    var linkedChannelMemberReadModel =
                     await queryProcessor.ProcessAsync(
                            new GetChannelMemberByUserIdQuery(linkedChannelReadModel.ChannelId, input.UserId));
                    linkedChannel = chatConverterService.ToChannel(input,
                      linkedChannelReadModel, linkedChannelPhotoReadModel, linkedChannelMemberReadModel,
                      linkedChannelMemberReadModel == null || linkedChannelMemberReadModel.Left, input.Layer);

                    //r.Chats.Add(linkedChannel);
                }
            }

            if (linkedChannel != null)
            {
                chatFull.Chats.Add(linkedChannel);
            }

            return chatFull;
        }

        throw new NotImplementedException();
    }

    private async Task SetRecentRequestersAsync(IRequestInput input, ILayeredChannelFull layeredChannelFull, MyTelegram.Schema.Messages.IChatFull chatFull)
    {
        var channelId = layeredChannelFull.Id;
        if (await channelAdminRightsChecker.HasChatAdminRightAsync(channelId, input.UserId,
                p => p.AdminRights.InviteUsers))
        {
            var pendingRequestsCount =
                await queryProcessor.ProcessAsync(new GetPendingRequestsCountQuery(channelId));
            if (pendingRequestsCount > 0)
            {
                layeredChannelFull.RequestsPending = pendingRequestsCount;
                var recentRequesters =
                    await queryProcessor.ProcessAsync(
                        new GetRecentRequestUserIdListQuery(channelId, 5));
                layeredChannelFull.RecentRequesters = [.. recentRequesters];

                var users = await userConverterService.GetUserListAsync(input, [.. recentRequesters], false, false, input.Layer);
                chatFull.Users = [.. users];
            }
        }
    }
}
