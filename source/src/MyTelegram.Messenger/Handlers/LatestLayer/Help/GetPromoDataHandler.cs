namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Returns a set of useful suggestions and PSA/MTProxy sponsored peers, see <a href="https://corefork.telegram.org/api/config#suggestions">here »</a> for more info.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getPromoData"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPromoDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPromoData, MyTelegram.Schema.Help.IPromoData>
{
    protected override Task<IPromoData> HandleCoreAsync(IRequestInput input, RequestGetPromoData obj)
    {
        IPromoData r = new TPromoDataEmpty
        {
            Expires = (int)DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
        };
        return Task.FromResult(r);
    }
}