// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.getDialogFilters" />
///</summary>
internal sealed class GetDialogFiltersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogFilters, TVector<MyTelegram.Schema.IDialogFilter>>,
    Messages.IGetDialogFiltersHandler
{
    protected override Task<TVector<MyTelegram.Schema.IDialogFilter>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDialogFilters obj)
    {
        throw new NotImplementedException();
    }
}
