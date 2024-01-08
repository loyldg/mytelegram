// ReSharper disable All

using MyTelegram.Schema.Updates;

namespace MyTelegram.Handlers.Updates;

///<summary>
/// Returns a current state of updates.
/// See <a href="https://corefork.telegram.org/method/updates.getState" />
///</summary>
internal sealed class GetStateHandler : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetState, MyTelegram.Schema.Updates.IState>,
    Updates.IGetStateHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILogger<GetStateHandler> _logger;

    public GetStateHandler(IPtsHelper ptsHelper,
        IQueryProcessor queryProcessor,
        IObjectMapper objectMapper, ILogger<GetStateHandler> logger)
    {
        _ptsHelper = ptsHelper;
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
        _logger = logger;
    }

    protected override async Task<IState> HandleCoreAsync(IRequestInput input,
        RequestGetState obj)
    {
        if (input.UserId == 0)
        {
            RpcErrors.RpcErrors401.AuthKeyInvalid.ThrowRpcError();
        }
        //var userId = await GetUidAsync(input);
        var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId), default);
        TState state;
        if (pts == null)
        {
            state = new TState
            {
                Date = DateTime.UtcNow.ToTimestamp(),
                Pts = 0,
                Qts = 0,
                Seq = 0,
                UnreadCount = 0,
            };
        }
        else
        {
            state = _objectMapper.Map<IPtsReadModel, TState>(pts);
            var cachedPts = _ptsHelper.GetCachedPts(input.UserId);
            if (cachedPts > 0 && cachedPts != pts.Pts)
            {
                state.Pts = cachedPts;
            }
        }

        _logger.LogInformation("Get state:{@state}", state);


        return state;
    }
}
