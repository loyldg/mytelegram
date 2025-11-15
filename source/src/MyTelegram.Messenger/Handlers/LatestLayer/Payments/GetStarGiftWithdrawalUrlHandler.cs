namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Convert a <a href="https://corefork.telegram.org/api/gifts">collectible gift »</a> to an NFT on the TON blockchain.
/// Possible errors
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 PASSWORD_TOO_FRESH_%d The password was modified less than 24 hours ago, try again in %d seconds.
/// 400 SESSION_TOO_FRESH_%d This session was created less than 24 hours ago, try again in %d seconds.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarGiftWithdrawalUrl"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarGiftWithdrawalUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftWithdrawalUrl, MyTelegram.Schema.Payments.IStarGiftWithdrawalUrl>
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftWithdrawalUrl> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftWithdrawalUrl obj)
    {
        throw new NotImplementedException();
    }
}