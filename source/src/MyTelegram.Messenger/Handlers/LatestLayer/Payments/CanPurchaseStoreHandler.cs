namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Checks whether a purchase is possible. Must be called before in-store purchase, official apps only.
/// Possible errors
/// Code Type Description
/// 400 INPUT_PURPOSE_INVALID The specified payment purpose is invalid.
/// 406 PREMIUM_CURRENTLY_UNAVAILABLE You cannot currently purchase a Premium subscription.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.canPurchaseStore"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class CanPurchaseStoreHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCanPurchaseStore, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestCanPurchaseStore obj)
    {
        throw new NotImplementedException();
    }
}