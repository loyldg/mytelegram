using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SaveDraftHandler : RpcResultObjectHandler<RequestSaveDraft, IBool>,
    ISaveDraftHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public SaveDraftHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveDraft obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var dialogId = DialogId.Create(input.UserId, peer);
        var saveDraftCommand = new SaveDraftCommand(dialogId,
            input.ReqMsgId,
            obj.Message,
            obj.NoWebpage,
            CurrentDate,
            obj.ReplyToMsgId,
            null);
        await _commandBus.PublishAsync(saveDraftCommand, CancellationToken.None);

        return new TBoolTrue();
    }
}