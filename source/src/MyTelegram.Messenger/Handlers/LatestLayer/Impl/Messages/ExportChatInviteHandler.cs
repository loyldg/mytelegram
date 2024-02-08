// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Export an invite link for a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 EXPIRE_DATE_INVALID The specified expiration date is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.exportChatInvite" />
///</summary>
internal sealed class ExportChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestExportChatInvite, MyTelegram.Schema.IExportedChatInvite>,
    Messages.IExportChatInviteHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IIdGenerator _idGenerator;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IChatInviteLinkHelper _chatInviteLinkHelper;
    private readonly IChannelAdminRightsChecker _channelAdminRightsChecker;
    public ExportChatInviteHandler(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper, IIdGenerator idGenerator, IQueryProcessor queryProcessor, IChatInviteLinkHelper chatInviteLinkHelper, IChannelAdminRightsChecker channelAdminRightsChecker)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
        _idGenerator = idGenerator;
        _queryProcessor = queryProcessor;
        _chatInviteLinkHelper = chatInviteLinkHelper;
        _channelAdminRightsChecker = channelAdminRightsChecker;
    }

    protected override async Task<MyTelegram.Schema.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestExportChatInvite obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel.ChannelId, inputPeerChannel.AccessHash);

            var chatInviteId = await _idGenerator.NextLongIdAsync(IdType.InviteId, inputPeerChannel.ChannelId);
            var inviteHash = _chatInviteLinkHelper.GenerateInviteLink();
            var channelReadModel =
                await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputPeerChannel.ChannelId));
            if (channelReadModel == null)
            {
                RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
            }

            await _channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId,
                (p) => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);

            var command = new CreateChatInviteCommand(ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteId),
                input.ToRequestInfo(),
                inputPeerChannel.ChannelId,
                chatInviteId,
                inviteHash,
                input.UserId,
                obj.Title,
                obj.RequestNeeded,
                null,
                obj.ExpireDate,
                obj.UsageLimit,
                false,
                CurrentDate
            );

            await _commandBus.PublishAsync(command, default);
            return null!;
        }

        throw new NotImplementedException();
    }
}
