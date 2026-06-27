namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Reorder pinned dialogs
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.reorderPinnedDialogs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReorderPinnedDialogsHandler(IDialogAppService dialogAppService, IPeerHelper peerHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderPinnedDialogs, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestReorderPinnedDialogs obj)
    {
        var peerList = new List<Peer>();
        foreach (var inputDialogPeer in obj.Order)
        {
            switch (inputDialogPeer)
            {
                case TInputDialogPeer inputDialogPeer1:
                    peerList.Add(peerHelper.GetPeer(inputDialogPeer1.Peer, input.UserId));
                    break;
                    //case TInputDialogPeerFolder inputDialogPeerFolder:
                    //    break;
                    //default:
                    //    throw new ArgumentOutOfRangeException(nameof(inputDialogPeer));
            }
        }

        await dialogAppService.ReorderPinnedDialogsAsync(new ReorderPinnedDialogsInput(input.UserId, peerList));
        return new TBoolTrue();
    }
}