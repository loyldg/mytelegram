namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Mark mentions as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readMentions" />
///</summary>
internal sealed class ReadMentionsHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor,
    IPtsHelper ptsHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadMentions, MyTelegram.Schema.Messages.IAffectedHistory>
{
    protected override async Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadMentions obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var dialogId = DialogId.Create(input.UserId, peer);

        var dialogReadModel = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(dialogId.Value));

        if (dialogReadModel != null && dialogReadModel.UnreadMentionsCount > 0)
        {
            var messageId = obj.TopMsgId ?? dialogReadModel.TopMessage;
            var command = new ReadMentionCommand(dialogId, input.ToRequestInfo(), input.UserId, messageId, true);
            await commandBus.PublishAsync(command);
            return null!;
        }

        return new TAffectedHistory
        {
            Pts = ptsHelper.GetCachedPts(input.UserId),
            PtsCount = 0,
            Offset = 0
        };
    }
}
