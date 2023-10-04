using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class ReorderPinnedDialogsHandler : RpcResultObjectHandler<RequestReorderPinnedDialogs, IBool>,
    IReorderPinnedDialogsHandler, IProcessedHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPeerHelper _peerHelper;

    public ReorderPinnedDialogsHandler(IDialogAppService dialogAppService,
        IPeerHelper peerHelper)
    {
        _dialogAppService = dialogAppService;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderPinnedDialogs obj)
    {
        var peerList = new List<Peer>();
        foreach (var inputDialogPeer in obj.Order)
            switch (inputDialogPeer)
            {
                case TInputDialogPeer inputDialogPeer1:
                    peerList.Add(_peerHelper.GetPeer(inputDialogPeer1.Peer, input.UserId));
                    break;
                //case TInputDialogPeerFolder inputDialogPeerFolder:
                //    break;
                //default:
                //    throw new ArgumentOutOfRangeException(nameof(inputDialogPeer));
            }

        await _dialogAppService.ReorderPinnedDialogsAsync(new ReorderPinnedDialogsInput(input.UserId, peerList))
            ;
        return new TBoolTrue();
    }
}