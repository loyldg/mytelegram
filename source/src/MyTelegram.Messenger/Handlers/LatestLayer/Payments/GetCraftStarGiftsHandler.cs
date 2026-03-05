
using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetCraftStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetCraftStarGifts, MyTelegram.Schema.Payments.ISavedStarGifts>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.ISavedStarGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetCraftStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.ISavedStarGifts>(new TSavedStarGifts
        {
            Chats = [],
            Users = [],
            Gifts = []
        });
    }
}

