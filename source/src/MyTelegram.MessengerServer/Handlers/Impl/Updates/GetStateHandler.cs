using MyTelegram.Handlers.Updates;
using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.Handlers.Impl.Updates;

public class GetStateHandler : RpcResultObjectHandler<RequestGetState, IState>,
    IGetStateHandler, IProcessedHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetStateHandler(IPtsHelper ptsHelper,
        IQueryProcessor queryProcessor,
        IObjectMapper objectMapper)
    {
        _ptsHelper = ptsHelper;
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
    }

    protected override async Task<IState> HandleCoreAsync(IRequestInput input,
        RequestGetState obj)
    {
        //var userId = await GetUidAsync(input);
        var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), CancellationToken.None)
            .ConfigureAwait(false);
        if (pts == null)
        {
            return new TState {
                Date = CurrentDate,
                Pts = 0,
                Qts = 0,
                Seq = 0,
                UnreadCount = 0
            };
        }

        var r = _objectMapper.Map<IPtsReadModel, TState>(pts);
        var cachedPts = _ptsHelper.GetCachedPts(input.UserId);
        if (cachedPts > 0 && cachedPts != pts.Pts)
        {
            r.Pts = cachedPts;
        }

        return r;
    }
}
