namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark messages as read in a <a href="https://corefork.telegram.org/api/monoforum">monoforum topic »</a>.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PARENT_PEER_INVALID The specified <code>parent_peer</code> is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.readSavedHistory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadSavedHistoryHandler(ICommandBus commandBus, IPeerHelper peerHelper, IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadSavedHistory, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReadSavedHistory obj)
    {
        if (obj.MaxId > 0)
        {
            var parentPeer = peerHelper.GetPeer(obj.ParentPeer);
            if (parentPeer.PeerType == PeerType.Channel)
            {
                var peer = peerHelper.GetPeer(obj.Peer);
                var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId.Create(parentPeer.PeerId, obj.MaxId).Value));
                if (messageReadModel == null)
                {
                    RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
                }

                var dialogReadModel = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, parentPeer).Value));
                if (dialogReadModel == null)
                {
                    return new TBoolTrue();
                }

                if (dialogReadModel!.ReadInboxMaxId >= obj.MaxId)
                {
                    return new TBoolTrue();
                }

                var command = new UpdateReadChannelInboxCommand(DialogId.Create(input.UserId, PeerType.Channel, parentPeer.PeerId), input.ToRequestInfo(), messageReadModel!.SenderUserId, obj.MaxId);
                await commandBus.PublishAsync(command);
                return null!;
            }
        }

        return new TBoolTrue();
    }
}