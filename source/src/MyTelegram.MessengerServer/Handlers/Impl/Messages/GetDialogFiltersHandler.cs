using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDialogFiltersHandler : RpcResultObjectHandler<RequestGetDialogFilters, TVector<IDialogFilter>>,
    IGetDialogFiltersHandler, IProcessedHandler
{
    protected override Task<TVector<IDialogFilter>> HandleCoreAsync(IRequestInput input,
        RequestGetDialogFilters obj)
    {
        var filters = new TVector<IDialogFilter>();

        return Task.FromResult(filters);
    }
}
