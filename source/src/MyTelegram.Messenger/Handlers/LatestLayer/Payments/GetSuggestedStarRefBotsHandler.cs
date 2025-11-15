namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain a list of suggested <a href="https://corefork.telegram.org/api/bots/webapps">mini apps</a> with available <a href="https://corefork.telegram.org/api/bots/referrals">affiliate programs</a><code>order_by_revenue</code> and <code>order_by_date</code> are mutually exclusive: if neither is set, results are sorted by profitability.
/// Possible errors
/// Code Type Description
/// 403 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getSuggestedStarRefBots"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSuggestedStarRefBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetSuggestedStarRefBots, MyTelegram.Schema.Payments.ISuggestedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.ISuggestedStarRefBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetSuggestedStarRefBots obj)
    {
        throw new NotImplementedException();
    }
}