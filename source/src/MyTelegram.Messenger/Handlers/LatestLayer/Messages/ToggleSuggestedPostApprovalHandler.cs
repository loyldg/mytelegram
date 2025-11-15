namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Approve or reject a <a href="https://corefork.telegram.org/api/suggested-posts">suggested post »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleSuggestedPostApproval"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleSuggestedPostApprovalHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleSuggestedPostApproval, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestToggleSuggestedPostApproval obj)
    {
        throw new NotImplementedException();
    }
}