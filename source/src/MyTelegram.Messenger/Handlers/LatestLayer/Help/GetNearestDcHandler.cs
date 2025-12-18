namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Returns info on data center nearest to the user.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getNearestDc"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class GetNearestDcHandler(IOptions<MyTelegramMessengerServerOptions> options) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetNearestDc, MyTelegram.Schema.INearestDc>
{
    protected override Task<INearestDc> HandleCoreAsync(IRequestInput input, RequestGetNearestDc obj)
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