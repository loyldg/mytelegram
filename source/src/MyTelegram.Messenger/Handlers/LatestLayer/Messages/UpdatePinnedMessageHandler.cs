// ReSharper disable All
using GetSimpleMessageListQuery = MyTelegram.Queries.GetSimpleMessageListQuery;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Pin a message
/// Possible errors
/// Code Type Description
/// 400 BOT_ONESIDE_NOT_AVAIL Bots can't pin messages in PM just for themselves.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 BUSINESS_PEER_INVALID Messages can't be set to the specified peer through the current <a href="https://corefork.telegram.org/api/business#connected-bots">business connection</a>.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_INVALID Invalid chat.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PIN_RESTRICTED You can't pin messages.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.updatePinnedMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class UpdatePinnedMessageHandler(ICommandBus commandBus, IPeerHelper peerHelper, IChannelAppService channelAppService, IQueryProcessor queryProcessor, IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdatePinnedMessage, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestUpdatePinnedMessage obj)
    {
        var userId = input.ConnectedBotData?.UserId ?? input.UserId;
        var peer = peerHelper.GetPeer(obj.Peer, userId);
        if (peer.PeerType == PeerType.Channel)
        {
            var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
            // Broadcast channels require the edit messages right, supergroups require the pin messages right
            var isAdminWithPinRight = await channelAdminRightsChecker.HasChatAdminRightAsync(peer.PeerId, userId,
                rights => channelReadModel.Broadcast ? rights.EditMessages : rights.PinMessages);
            if (!isAdminWithPinRight)
            {
                // Supergroup members can pin messages if the default rights allow it
                var canMemberPin = !channelReadModel.Broadcast &&
                                   !(channelReadModel.DefaultBannedRights?.PinMessages ?? true) &&
                                   await channelAppService.IsChannelMemberAsync(userId, peer.PeerId);
                if (!canMemberPin)
                {
                    RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
                }
            }
        }

        var messageItems = await queryProcessor.ProcessAsync(new GetSimpleMessageListQuery(userId, peer, [obj.Id], null, !obj.PmOneside, MyTelegramConsts.UnPinAllMessagesDefaultPageSize));
        if (messageItems.Count == 0)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var command = new StartUpdatePinnedMessagesCommand(TempId.New, input.ToRequestInfo(), messageItems, peer, !obj.Unpin, obj.PmOneside);
        await commandBus.PublishAsync(command);
        return null!;
    }
}