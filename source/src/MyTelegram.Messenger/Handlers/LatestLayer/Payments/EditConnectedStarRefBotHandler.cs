namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Leave a bot's <a href="https://corefork.telegram.org/api/bots/referrals#becoming-an-affiliate">affiliate program »</a>
/// Possible errors
/// Code Type Description
/// 400 STARREF_HASH_REVOKED The specified affiliate link was already revoked.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.editConnectedStarRefBot"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditConnectedStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestEditConnectedStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestEditConnectedStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}