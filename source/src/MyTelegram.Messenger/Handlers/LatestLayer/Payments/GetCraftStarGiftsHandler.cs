using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getCraftStarGifts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetCraftStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetCraftStarGifts, MyTelegram.Schema.Payments.ISavedStarGifts>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Payments.ISavedStarGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetCraftStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.ISavedStarGifts>(new TSavedStarGifts { Chats = [], Users = [], Gifts = [] });
    }
}