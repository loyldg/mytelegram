namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/giveaways">Telegram Premium giveaway »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.getGiveawayInfo" />
///</summary>
internal sealed class GetGiveawayInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetGiveawayInfo, MyTelegram.Schema.Payments.IGiveawayInfo>
{
    protected override Task<MyTelegram.Schema.Payments.IGiveawayInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetGiveawayInfo obj)
    {
        throw new NotImplementedException();
    }
}
