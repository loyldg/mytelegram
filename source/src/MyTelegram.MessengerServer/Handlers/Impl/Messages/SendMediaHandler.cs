using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendMediaHandler : RpcResultObjectHandler<RequestSendMedia, IUpdates>,
    ISendMediaHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly IMediaHelper _mediaHelper;
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;

    public SendMediaHandler(IMediaHelper mediaHelper,
        IMessageAppService messageAppService,
        IPeerHelper peerHelper)
    {
        _mediaHelper = mediaHelper;
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendMedia obj)
    {
        var media = await _mediaHelper.SaveMediaAsync(obj.Media).ConfigureAwait(false);
        var sendMessageInput = new SendMessageInput(input.ToRequestInfo(),
            input.UserId,
            _peerHelper.GetPeer(obj.Peer, input.UserId),
            obj.Message,
            obj.RandomId,
            clearDraft: obj.ClearDraft,
            entities: obj.Entities.ToBytes(),
            media: media.ToBytes(),
            replyToMsgId: obj.ReplyToMsgId,
            sendMessageType: SendMessageType.Media,
            messageType: _mediaHelper.GeMessageType(media)
        );
        await _messageAppService.SendMessageAsync(sendMessageInput).ConfigureAwait(false);

        return null!;
    }
}
