namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Export an invite link for a chat
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_MONOFORUM_UNSUPPORTED <a href="https://corefork.telegram.org/api/channel#monoforums">Monoforums</a> do not support this feature.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 EXPIRE_DATE_INVALID The specified expiration date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PRICING_CHAT_INVALID The pricing for the <a href="https://corefork.telegram.org/api/subscriptions">subscription</a> is invalid, the maximum price is specified in the <a href="https://corefork.telegram.org/api/config#stars-subscription-amount-max"><code>stars_subscription_amount_max</code> config key »</a>.
/// 400 SUBSCRIPTION_PERIOD_INVALID The specified subscription_pricing.period is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.exportChatInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ExportChatInviteHandler(ICommandBus commandBus,
    IIdGenerator idGenerator,
    IChannelAppService channelAppService,
    IQueryProcessor queryProcessor,
    IChatInviteLinkHelper chatInviteLinkHelper, IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestExportChatInvite, MyTelegram.Schema.IExportedChatInvite>
{
    protected override async Task<MyTelegram.Schema.IExportedChatInvite> HandleCoreAsync(IRequestInput input, RequestExportChatInvite obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            var chatInviteId = await idGenerator.NextLongIdAsync(IdType.InviteId, inputPeerChannel.ChannelId);
            var inviteHash = chatInviteLinkHelper.GenerateInviteLink();
            var channelReadModel = await channelAppService.GetAsync(inputPeerChannel.ChannelId);
            if (channelReadModel == null!)
            {
                RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
            }
            await channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId, (p) => p.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);
            if (obj.LegacyRevokePermanent)
            {
                var chatInviteReadModel =
                    await queryProcessor.ProcessAsync(new GetPermanentChatInviteQuery(channelReadModel!.ChannelId,
                        input.UserId));
                if (chatInviteReadModel != null)
                {
                    var revokeChatInviteCommand =
                        new RevokeChatInviteCommand(ChatInviteId.Create(channelReadModel.ChannelId,
                            chatInviteReadModel.InviteId));
                    await commandBus.PublishAsync(revokeChatInviteCommand);
                }
            }

            var command = new CreateChatInviteCommand(ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteId), input.ToRequestInfo(), inputPeerChannel.ChannelId, chatInviteId, inviteHash, input.UserId, obj.Title, obj.RequestNeeded, null, obj.ExpireDate, obj.UsageLimit, obj.LegacyRevokePermanent, CurrentDate, channelReadModel!.Broadcast);
            await commandBus.PublishAsync(command);
            return null!;
        }

        throw new NotImplementedException();
    }
}