namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get messages in a reply thread
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getReplies" />
///</summary>
internal sealed class GetRepliesHandler(
    IPeerHelper peerHelper,
    IMessageAppService messageAppService,
    IAccessHashHelper accessHashHelper,
    IGetHistoryConverterService getHistoryConverterService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetReplies, MyTelegram.Schema.Messages.IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetReplies obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = peerHelper.GetPeer(obj.Peer);
        var getMessageOutput = await messageAppService.GetRepliesAsync(new GetRepliesInput
        {
            ReplyToMsgId = obj.MsgId,
            OwnerPeerId = peer.PeerId,
            AddOffset = obj.AddOffset,
            Limit = obj.Limit,
            OffsetId = obj.OffsetId,
            MinDate = obj.OffsetDate,
            SelfUserId = input.UserId
        });

        return getHistoryConverterService.ToMessages(input, getMessageOutput, input.Layer);
    }
}
