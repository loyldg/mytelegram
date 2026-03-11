namespace MyTelegram.Messenger.Handlers.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getStarGiftAuctionState"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStarGiftAuctionStateHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftAuctionState, MyTelegram.Schema.Payments.IStarGiftAuctionState>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftAuctionState> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftAuctionState obj)
    {
        throw new NotImplementedException();
    }
}