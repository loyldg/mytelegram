
namespace MyTelegram.Messenger.Handlers.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStarGiftAuctionStateHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftAuctionState, MyTelegram.Schema.Payments.IStarGiftAuctionState>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGiftAuctionState> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftAuctionState obj)
    {
        throw new NotImplementedException();
    }
}

