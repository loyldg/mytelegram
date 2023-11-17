// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Reorder pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reorderPinnedDialogs" />
///</summary>
internal sealed class ReorderPinnedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderPinnedDialogs, IBool>,
    Messages.IReorderPinnedDialogsHandler
{
    private readonly IDialogAppService _dialogAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public ReorderPinnedDialogsHandler(IDialogAppService dialogAppService,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _dialogAppService = dialogAppService;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderPinnedDialogs obj)
    {
        var peerList = new List<Peer>();
        foreach (var inputDialogPeer in obj.Order)
        {
            switch (inputDialogPeer)
            {
                case TInputDialogPeer inputDialogPeer1:
                    await _accessHashHelper.CheckAccessHashAsync(inputDialogPeer1.Peer);
                    peerList.Add(_peerHelper.GetPeer(inputDialogPeer1.Peer, input.UserId));
                    break;
                //case TInputDialogPeerFolder inputDialogPeerFolder:
                //    break;
                //default:
                //    throw new ArgumentOutOfRangeException(nameof(inputDialogPeer));
            }
        }

        await _dialogAppService.ReorderPinnedDialogsAsync(new ReorderPinnedDialogsInput(input.UserId, peerList))
            ;
        return new TBoolTrue();
    }
}
