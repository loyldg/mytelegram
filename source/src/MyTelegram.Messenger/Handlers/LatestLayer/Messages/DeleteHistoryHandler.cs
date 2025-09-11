namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Deletes communication history.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_REVOKE_DATE_UNSUPPORTED <code>min_date</code> and <code>max_date</code> are not available for using with non-user peers.
/// 400 MAX_DATE_INVALID The specified maximum date is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MIN_DATE_INVALID The specified minimum date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteHistory" />
///</summary>
internal sealed class DeleteHistoryHandler(
    ICommandBus commandBus,
    //IRandomHelper randomHelper,
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IPtsHelper ptsHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteHistory,
            MyTelegram.Schema.Messages.IAffectedHistory>
{
    protected override async Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteHistory obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var pageSize = MyTelegramConsts.ClearHistoryDefaultPageSize;
        var maxId = obj.MaxId;
        if (maxId > 0)
        {
            maxId = maxId + 1;
        }

        var messageItemsToBeDeleted =
            await queryProcessor.ProcessAsync(
                new GetMessageItemListToBeDeletedQuery2(input.UserId, peer.PeerId, maxId, pageSize, obj.Revoke));

        if (messageItemsToBeDeleted.Count == 0)
        {
            var cachedPts = ptsHelper.GetCachedPts(input.UserId);
            return new TAffectedHistory { Offset = 0, Pts = cachedPts, PtsCount = 0 };
        }

        switch (peer.PeerType)
        {
            case PeerType.Chat:
                {
                }
                break;
            case PeerType.User:
                {
                    if (obj.Peer is TInputPeerUser inputUser)
                    {
                        await accessHashHelper.CheckAccessHashAsync(input, inputUser.UserId, inputUser.AccessHash, AccessHashType.User);
                    }
                }
                break;
        }

        var command = new StartDeleteHistoryCommand(TempId.New, input.ToRequestInfo(), messageItemsToBeDeleted,
            obj.Revoke, obj.Revoke);
        await commandBus.PublishAsync(command);

        return null!;
    }
}
