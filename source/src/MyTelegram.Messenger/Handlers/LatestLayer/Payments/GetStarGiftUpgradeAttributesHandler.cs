namespace MyTelegram.Messenger.Handlers.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarGiftUpgradeAttributes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarGiftUpgradeAttributesHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftUpgradeAttributes, MyTelegram.Schema.Payments.IStarGiftUpgradeAttributes>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftUpgradeAttributes> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftUpgradeAttributes obj)
    {
        throw new NotImplementedException();
    }
}