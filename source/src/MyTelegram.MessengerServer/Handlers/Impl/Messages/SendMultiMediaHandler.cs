using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendMultiMediaHandler : RpcResultObjectHandler<RequestSendMultiMedia, IUpdates>,
    ISendMultiMediaHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly IMediaHelper _mediaHelper;

    private readonly IMessageAppService _messageAppService;

    //private readonly IRequestCacheAppService _requestCacheAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public SendMultiMediaHandler(IMessageAppService messageAppService,
        IMediaHelper mediaHelper,
        //IRequestCacheAppService requestCacheAppService,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper)
    {
        _messageAppService = messageAppService;
        _mediaHelper = mediaHelper;
        //_requestCacheAppService = requestCacheAppService;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendMultiMedia obj)
    {
        var groupId = _randomHelper.NextLong();
        var groupItemCount = obj.MultiMedia.Count;
        var requestInfo = input.ToRequestInfo();
        foreach (var inputSingleMedia in obj.MultiMedia)
        {
            var media = await _mediaHelper.SaveMediaAsync(inputSingleMedia.Media).ConfigureAwait(false);
            var sendMessageInput = new SendMessageInput(requestInfo,
                input.UserId,
                _peerHelper.GetPeer(obj.Peer, input.UserId),
                inputSingleMedia.Message,
                inputSingleMedia.RandomId,
                clearDraft: obj.ClearDraft,
                entities: inputSingleMedia.Entities.ToBytes(),
                media: media.ToBytes(),
                replyToMsgId: obj.ReplyToMsgId,
                sendMessageType: SendMessageType.Media,
                messageType: _mediaHelper.GeMessageType(media),
                groupId: groupId,
                groupItemCount: groupItemCount
            );
            await _messageAppService.SendMessageAsync(sendMessageInput).ConfigureAwait(false);
            //_requestCacheAppService.AddRequest(input.ReqMsgId, input.AuthKeyId, input.RequestSessionId, input.SeqNumber);
        }

        return null!;
    }
}
