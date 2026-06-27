namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark a <a href="https://corefork.telegram.org/api/threads">thread</a> as read
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.readDiscussion"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadDiscussionHandler(ICommandBus commandBus, IPeerHelper peerHelper, IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadDiscussion, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestReadDiscussion obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var selfDialogId = DialogId.Create(input.UserId, peer);
        var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId.Create(peer.PeerId, obj.MsgId).Value));
        if (messageReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var dialogReadModel = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, peer).Value));
        if (dialogReadModel == null)
        {
            return new TBoolTrue();
            //RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        }

        if (dialogReadModel!.ReadInboxMaxId >= obj.ReadMaxId)
        {
            return new TBoolFalse();
        }

        var command = new UpdateReadChannelInboxCommand(selfDialogId, input.ToRequestInfo(), messageReadModel!.SenderUserId, obj.ReadMaxId);
        await commandBus.PublishAsync(command);
        return null!;
    }
}