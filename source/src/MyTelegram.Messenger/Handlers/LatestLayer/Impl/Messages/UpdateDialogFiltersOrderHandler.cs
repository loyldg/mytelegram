// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Reorder <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFiltersOrder" />
///</summary>
internal sealed class UpdateDialogFiltersOrderHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateDialogFiltersOrder, IBool>,
    Messages.IUpdateDialogFiltersOrderHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUpdateDialogFiltersOrder obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
