// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns info on data center nearest to the user.
/// See <a href="https://corefork.telegram.org/method/help.getNearestDc" />
///</summary>
internal sealed class GetNearestDcHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetNearestDc, MyTelegram.Schema.INearestDc>,
    Help.IGetNearestDcHandler
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
            Country = "US",
            NearestDc = _options.Value.ThisDcId,
            ThisDc = _options.Value.ThisDcId
        };

        return Task.FromResult(r);
    }
}
