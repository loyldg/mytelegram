using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetRepliesHandler : RpcResultObjectHandler<RequestGetReplies, IMessages>,
    IGetRepliesHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetRepliesHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetReplies obj)
    {
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
        return _rpcResultProcessor.ToMessages(r);
    }
}
