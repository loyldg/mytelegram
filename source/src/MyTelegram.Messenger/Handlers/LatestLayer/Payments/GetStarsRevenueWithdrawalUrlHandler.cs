namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Withdraw funds from a channel or bot's <a href="https://corefork.telegram.org/api/stars#withdrawing-revenue">star balance »</a>.
/// Possible errors
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 PASSWORD_MISSING You must <a href="https://corefork.telegram.org/api/srp">enable 2FA</a> before executing this operation.
/// 400 PASSWORD_TOO_FRESH_%d The password was modified less than 24 hours ago, try again in %d seconds.
/// 400 SESSION_TOO_FRESH_%d This session was created less than 24 hours ago, try again in %d seconds.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarsRevenueWithdrawalUrl"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarsRevenueWithdrawalUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsRevenueWithdrawalUrl, MyTelegram.Schema.Payments.IStarsRevenueWithdrawalUrl>
{
    protected override Task<MyTelegram.Schema.Payments.IStarsRevenueWithdrawalUrl> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarsRevenueWithdrawalUrl obj)
    {
        throw new NotImplementedException();
    }
}