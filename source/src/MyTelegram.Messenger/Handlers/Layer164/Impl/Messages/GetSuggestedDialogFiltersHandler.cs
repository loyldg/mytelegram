// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/folders">suggested folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.getSuggestedDialogFilters" />
///</summary>
internal sealed class GetSuggestedDialogFiltersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSuggestedDialogFilters, TVector<MyTelegram.Schema.IDialogFilterSuggested>>,
    Messages.IGetSuggestedDialogFiltersHandler
{
    protected override Task<TVector<MyTelegram.Schema.IDialogFilterSuggested>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSuggestedDialogFilters obj)
    {
        var r = new TVector<IDialogFilterSuggested>();

        return Task.FromResult(r);
    }
}
