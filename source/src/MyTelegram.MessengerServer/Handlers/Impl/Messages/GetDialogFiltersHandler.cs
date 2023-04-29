using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDialogFiltersHandler : RpcResultObjectHandler<RequestGetDialogFilters, TVector<IDialogFilter>>,
    IGetDialogFiltersHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IObjectMapper _objectMapper;
    public GetDialogFiltersHandler(IQueryProcessor queryProcessor,
        IObjectMapper objectMapper)
    {
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
    }

    protected override async Task<TVector<IDialogFilter>> HandleCoreAsync(IRequestInput input,
        RequestGetDialogFilters obj)
    {
        var filterReadModels = await _queryProcessor.ProcessAsync(new GetDialogFiltersQuery(input.UserId), default)
            ;

        var filters = new TVector<IDialogFilter>();
        foreach (var filterReadModel in filterReadModels)
        {
            var filter = _objectMapper.Map<DialogFilter, TDialogFilter>(filterReadModel.Filter);
            filters.Add(filter);
        }

        return filters;
    }
}
