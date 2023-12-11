// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

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
internal sealed class GetRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetReplies, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetRepliesHandler
{
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;

    public GetRepliesHandler(
        IPeerHelper peerHelper,
        IMessageAppService messageAppService,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _peerHelper = peerHelper;
        _messageAppService = messageAppService;
        _accessHashHelper = accessHashHelper;
        _layeredService = layeredService;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetReplies obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer);
        var r = await _messageAppService.GetRepliesAsync(new GetRepliesInput
        {
            ReplyToMsgId = obj.MsgId,
            OwnerPeerId = peer.PeerId,
            AddOffset = obj.AddOffset,
            Limit = obj.Limit,
            //OffsetId = obj.OffsetId,
            MinDate = obj.OffsetDate,
            SelfUserId = input.UserId
        });
        return _layeredService.GetConverter(input.Layer).ToMessages(r, input.Layer);
    }
}
