namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/giveaways">Telegram Premium giveaway »</a>.
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getGiveawayInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGiveawayInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetGiveawayInfo, MyTelegram.Schema.Payments.IGiveawayInfo>
{
    protected override Task<MyTelegram.Schema.Payments.IGiveawayInfo> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetGiveawayInfo obj)
    {
        throw new NotImplementedException();
    }
}