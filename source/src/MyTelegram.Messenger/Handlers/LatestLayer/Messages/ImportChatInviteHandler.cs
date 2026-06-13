namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Import a chat invite and join a private chat/supergroup/channel
/// Possible errors
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_INVALID Invalid chat.
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// 400 INVITE_REQUEST_SENT You have successfully requested to join this chat or channel.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STARS_PAYMENT_REQUIRED To import this chat invite link, you must first <a href="https://corefork.telegram.org/api/subscriptions#channel-subscriptions">pay for the associated Telegram Star subscription »</a>.
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.importChatInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ImportChatInviteHandler(ICommandBus commandBus, IChannelAppService channelAppService, IQueryProcessor queryProcessor) : RpcResultObjectHandler<RequestImportChatInvite, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestImportChatInvite obj)
    {
        if (string.IsNullOrEmpty(obj.Hash))
        {
            RpcErrors.RpcErrors400.InviteHashEmpty.ThrowRpcError();
        }

        var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Hash));
        if (chatInviteReadModel == null)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        var channelId = chatInviteReadModel!.PeerId;
        if (chatInviteReadModel.ExpireDate > 0)
        {
            if (chatInviteReadModel.ExpireDate.Value < CurrentDate)
            {
                RpcErrors.RpcErrors400.InviteHashExpired.ThrowRpcError();
            }
        }

        if (chatInviteReadModel.Revoked)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        if (chatInviteReadModel.UsageLimit > 0)
        {
            if (chatInviteReadModel.Usage >= chatInviteReadModel.UsageLimit)
            {
                RpcErrors.RpcErrors400.UsersTooMuch.ThrowRpcError();
            }
        }

        var channelReadModel = await channelAppService.GetAsync(channelId);
        var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == chatInviteReadModel.AdminId);
        if (admin == null)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        var channelMember = await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(channelId, input.UserId));
        if (channelMember is { Left: false, Kicked: false })
        {
            RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
        }

        if (channelMember is { Kicked: true })
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        var joinRequestReadModel = await queryProcessor.ProcessAsync(new GetJoinRequestQuery(chatInviteReadModel.PeerId, input.UserId));
        if (joinRequestReadModel is { IsJoinRequestProcessed: false })
        {
            RpcErrors.RpcErrors400.InviteRequestSent.ThrowRpcError();
        }

        var requestState = chatInviteReadModel.RequestNeeded ? ChatInviteRequestState.WaitingForApproval : ChatInviteRequestState.NoApprovalRequired;
        if (!chatInviteReadModel.RequestNeeded)
        {
            if (channelReadModel.JoinRequest)
            {
                requestState = ChatInviteRequestState.WaitingForApproval;
            }
        }

        var command = new ImportChatInviteCommand(ChatInviteId.Create(chatInviteReadModel.PeerId, chatInviteReadModel.InviteId), input.ToRequestInfo(), requestState, CurrentDate);
        await commandBus.PublishAsync(command);
        return null !;
    }
}