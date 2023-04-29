using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetNearestDcHandler : RpcResultObjectHandler<RequestGetNearestDc, INearestDc>,
    IGetNearestDcHandler, IProcessedHandler
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public GetNearestDcHandler(IOptions<MyTelegramMessengerServerOptions> options)
    {
        _options = options;
    }

    protected override Task<INearestDc> HandleCoreAsync(IRequestInput input,
        RequestGetNearestDc obj)
    {
        INearestDc r = new TNearestDc
        {
            Country = "US", NearestDc = _options.Value.ThisDcId, ThisDc = _options.Value.ThisDcId
        };

        return Task.FromResult(r);
    }
}
