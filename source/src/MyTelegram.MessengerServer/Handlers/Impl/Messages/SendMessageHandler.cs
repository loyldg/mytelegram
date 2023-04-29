using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SendMessageHandler : RpcResultObjectHandler<RequestSendMessage, IUpdates>,
    ISendMessageHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;

    public SendMessageHandler(IMessageAppService messageAppService,
        IPeerHelper peerHelper)
    {
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendMessage obj)
    {
        var sendMessageInput = new SendMessageInput(input.ToRequestInfo(),
            input.UserId,
            _peerHelper.GetPeer(obj.Peer, input.UserId),
            obj.Message,
            obj.RandomId,
            obj.Entities.ToBytes(),
            obj.ReplyToMsgId,
            obj.ClearDraft
        );

        await _messageAppService.SendMessageAsync(sendMessageInput);

        return null!;
    }
}
