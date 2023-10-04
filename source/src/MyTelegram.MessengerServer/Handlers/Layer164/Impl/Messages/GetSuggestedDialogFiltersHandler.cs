using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetSuggestedDialogFiltersHandler :
    RpcResultObjectHandler<RequestGetSuggestedDialogFilters, TVector<IDialogFilterSuggested>>,
    IGetSuggestedDialogFiltersHandler, IProcessedHandler
{
    protected override Task<TVector<IDialogFilterSuggested>> HandleCoreAsync(IRequestInput input,
        RequestGetSuggestedDialogFilters obj)
    {
        var r = new TVector<IDialogFilterSuggested>();

        return Task.FromResult(r);
    }
}