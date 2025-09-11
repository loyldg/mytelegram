namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Refund a <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a> transaction, see <a href="https://corefork.telegram.org/api/payments#6-refunds">here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHARGE_ALREADY_REFUNDED The transaction was already refunded.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.refundStarsCharge" />
///</summary>
internal sealed class RefundStarsChargeHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestRefundStarsCharge, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestRefundStarsCharge obj)
    {
        throw new NotImplementedException();
    }
}
