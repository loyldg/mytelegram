
using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetStarGiftActiveAuctionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGiftActiveAuctions, MyTelegram.Schema.Payments.IStarGiftActiveAuctions>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.Payments.IStarGiftActiveAuctions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetStarGiftActiveAuctions obj)
    {
        return new TStarGiftActiveAuctions
        {
            Auctions = [],
            Chats = [],
            Users = []
        };
    }
}

