namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Returns info on data center nearest to the user.
/// See <a href="https://corefork.telegram.org/method/help.getNearestDc" />
///</summary>
internal sealed class GetNearestDcHandler(IOptions<MyTelegramMessengerServerOptions> options)
    : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetNearestDc, MyTelegram.Schema.INearestDc>
{
    protected override Task<INearestDc> HandleCoreAsync(IRequestInput input,
        RequestGetNearestDc obj)
    {
        INearestDc r = new TNearestDc
        {
            Country = "US",
            NearestDc = options.Value.ThisDcId,
            ThisDc = options.Value.ThisDcId
        };

        return Task.FromResult(r);
    }
}