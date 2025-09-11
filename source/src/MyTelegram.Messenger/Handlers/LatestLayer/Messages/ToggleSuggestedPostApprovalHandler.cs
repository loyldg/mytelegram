namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.toggleSuggestedPostApproval" />
///</summary>
internal sealed class ToggleSuggestedPostApprovalHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleSuggestedPostApproval, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleSuggestedPostApproval obj)
    {
        throw new NotImplementedException();
    }
}
