namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Fetch info about a specific <a href="https://corefork.telegram.org/api/bots/referrals">bot affiliation »</a>
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getConnectedStarRefBot"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetConnectedStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetConnectedStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetConnectedStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}