namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Join a bot's <a href="https://corefork.telegram.org/api/bots/referrals#becoming-an-affiliate">affiliate program, becoming an affiliate »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.connectStarRefBot"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ConnectStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestConnectStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestConnectStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}