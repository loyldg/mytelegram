namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/folders">suggested folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.getSuggestedDialogFilters" />
///</summary>
internal sealed class GetSuggestedDialogFiltersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSuggestedDialogFilters, TVector<MyTelegram.Schema.IDialogFilterSuggested>>
{
    protected override Task<TVector<IDialogFilterSuggested>> HandleCoreAsync(IRequestInput input,
        RequestGetSuggestedDialogFilters obj)
    {
        var r = new TVector<IDialogFilterSuggested>();

        return Task.FromResult(r);
    }
}
