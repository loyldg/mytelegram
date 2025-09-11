namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Launch a <a href="https://corefork.telegram.org/api/giveaways">prepaid giveaway »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.launchPrepaidGiveaway" />
///</summary>
internal sealed class LaunchPrepaidGiveawayHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway obj)
    {
        throw new NotImplementedException();
    }
}
