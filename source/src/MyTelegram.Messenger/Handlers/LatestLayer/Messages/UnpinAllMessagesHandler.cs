namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// <a href="https://corefork.telegram.org/api/pin">Unpin</a> all pinned messages
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.unpinAllMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class UnpinAllMessagesHandler(ICommandBus commandBus, IPeerHelper peerHelper, IChannelAdminRightsChecker channelAdminRightsChecker, IPtsHelper ptsHelper, IQueryProcessor queryProcessor, IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUnpinAllMessages, MyTelegram.Schema.Messages.IAffectedHistory>
{
    protected override async Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestUnpinAllMessages obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = peerHelper.GetPeer(obj.Peer);
        var ownerPeerId = input.UserId;
        if (peer.PeerType == PeerType.Channel)
        {
            await channelAdminRightsChecker.CheckAdminRightAsync(peer.PeerId, input.UserId, rights => rights is { PinMessages: true, EditMessages: true }, RpcErrors.RpcErrors400.ChatAdminRequired);
            ownerPeerId = peer.PeerId;
        }

        var messageItems = await queryProcessor.ProcessAsync(new GetSimpleMessageListQuery(ownerPeerId, peer, null, true, true, MyTelegramConsts.UnPinAllMessagesDefaultPageSize));
        if (messageItems.Count == 0)
        {
            return new TAffectedHistory
            {
                Pts = ptsHelper.GetCachedPts(ownerPeerId),
                PtsCount = 0,
                Offset = 0
            };
        }

        var command = new StartUnpinAllMessagesCommand(TempId.New, input.ToRequestInfo(), messageItems, peer);
        await commandBus.PublishAsync(command);
        return null !;
    }
}