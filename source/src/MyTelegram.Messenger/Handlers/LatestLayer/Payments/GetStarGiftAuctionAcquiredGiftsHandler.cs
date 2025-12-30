
namespace MyTelegram.Messenger.Handlers.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStarGiftAuctionAcquiredGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftAuctionAcquiredGifts, MyTelegram.Schema.Payments.IStarGiftAuctionAcquiredGifts>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftAuctionAcquiredGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftAuctionAcquiredGifts obj)
    {
        throw new NotImplementedException();
    }
}

