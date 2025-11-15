namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Launch a <a href="https://corefork.telegram.org/api/giveaways">prepaid giveaway »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.launchPrepaidGiveaway"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class LaunchPrepaidGiveawayHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestLaunchPrepaidGiveaway obj)
    {
        throw new NotImplementedException();
    }
}