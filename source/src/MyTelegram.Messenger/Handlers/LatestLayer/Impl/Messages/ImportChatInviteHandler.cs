// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Import a chat invite and join a private chat/supergroup/channel
/// <para>Possible errors</para>
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
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// See <a href="https://corefork.telegram.org/method/messages.importChatInvite" />
///</summary>
internal sealed class ImportChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestImportChatInvite, MyTelegram.Schema.IUpdates>,
    Messages.IImportChatInviteHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;
    public ImportChatInviteHandler(ICommandBus commandBus, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestImportChatInvite obj)
    {
        if (string.IsNullOrEmpty(obj.Hash))
        {
            RpcErrors.RpcErrors400.InviteHashEmpty.ThrowRpcError();
        }

        var chatInviteReadModel = await _queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Hash));
        if (chatInviteReadModel == null)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        if (chatInviteReadModel!.ExpireDate > 0)
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

        if (chatInviteReadModel.UsageLimit.HasValue)
        {
            if (chatInviteReadModel.Usage >= chatInviteReadModel.UsageLimit.Value)
            {
                RpcErrors.RpcErrors400.UsersTooMuch.ThrowRpcError();
            }
        }

        var channelMember =
            await _queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(chatInviteReadModel.PeerId,
                input.UserId));
        if (channelMember != null && !channelMember.Left && !channelMember.Kicked)
        {
            RpcErrors.RpcErrors400.UserAlreadyParticipant.ThrowRpcError();
        }

        var chatInviteImporterReadModel =
            await _queryProcessor.ProcessAsync(new GetChatInviteImporterQuery(chatInviteReadModel.PeerId,
                input.UserId));
        if (chatInviteImporterReadModel != null &&
            (chatInviteImporterReadModel.ChatInviteRequestState == ChatInviteRequestState.NeedApprove ||
             chatInviteImporterReadModel.ChatInviteRequestState == ChatInviteRequestState.Approved
             ))
        {
            RpcErrors.RpcErrors400.InviteRequestSent.ThrowRpcError();
        }

        var command = new ImportChatInviteCommand(ChatInviteId.Create(chatInviteReadModel.PeerId, chatInviteReadModel.InviteId),
            input.ToRequestInfo(),
            chatInviteReadModel.RequestNeeded ? ChatInviteRequestState.NeedApprove : ChatInviteRequestState.NotNeedApprove,
            CurrentDate
        );

        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
