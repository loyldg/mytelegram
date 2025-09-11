using RequestReadHistory = MyTelegram.Schema.Messages.RequestReadHistory;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Marks message history as read.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor,
    IPtsHelper ptsHelper)
    :
        RpcResultObjectHandler<RequestReadHistory, Schema.Messages.IAffectedMessages>
{
    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestReadHistory obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        if (obj.MaxId < 0)
        {
            return new TAffectedMessages
            {
                Pts = ptsHelper.GetCachedPts(input.UserId),
                PtsCount = 0
            };
        }

        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var messageReadModel =
            await queryProcessor.ProcessAsync(
                new GetMessageByIdQuery(MessageId.Create(input.UserId, obj.MaxId).Value));

        if (messageReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var selfDialogId = DialogId.Create(input.UserId, peer);
        var dialogReadModel = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(selfDialogId.Value));
        if (dialogReadModel == null || dialogReadModel.ReadInboxMaxId >= obj.MaxId)
        {
            return new TAffectedMessages
            {
                Pts = ptsHelper.GetCachedPts(input.UserId),
                PtsCount = 0
            };
        }

        var unreadCount =
            await queryProcessor.ProcessAsync(new GetUnreadCountQuery(input.UserId, peer.PeerId, obj.MaxId));

        var command = new UpdateReadInboxMaxIdCommand(selfDialogId, input.ToRequestInfo(), obj.MaxId,
            messageReadModel!.SenderUserId, messageReadModel.SenderMessageId, unreadCount);
        await commandBus.PublishAsync(command);

        return null!;
    }
}