namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Reorder pinned dialogs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reorderPinnedDialogs" />
///</summary>
internal sealed class ReorderPinnedDialogsHandler(
    IDialogAppService dialogAppService,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderPinnedDialogs, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderPinnedDialogs obj)
    {
        var peerList = new List<Peer>();
        foreach (var inputDialogPeer in obj.Order)
        {
            switch (inputDialogPeer)
            {
                case TInputDialogPeer inputDialogPeer1:
                    await accessHashHelper.CheckAccessHashAsync(input, inputDialogPeer1.Peer);
                    peerList.Add(peerHelper.GetPeer(inputDialogPeer1.Peer, input.UserId));
                    break;
                    //case TInputDialogPeerFolder inputDialogPeerFolder:
                    //    break;
                    //default:
                    //    throw new ArgumentOutOfRangeException(nameof(inputDialogPeer));
            }
        }

        await dialogAppService.ReorderPinnedDialogsAsync(new ReorderPinnedDialogsInput(input.UserId, peerList))
     ;
        return new TBoolTrue();
    }
}
