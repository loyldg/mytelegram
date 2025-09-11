namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Reorder <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFiltersOrder" />
///</summary>
internal sealed class UpdateDialogFiltersOrderHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateDialogFiltersOrder, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFiltersOrder obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
