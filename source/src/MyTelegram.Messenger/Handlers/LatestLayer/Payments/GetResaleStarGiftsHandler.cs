using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Get <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gifts</a> of a specific type currently on resale, see <a href="https://corefork.telegram.org/api/gifts#reselling-collectible-gifts">here »</a> for more info.<code>sort_by_price</code> and <code>sort_by_num</code> are mutually exclusive, if neither are set results are sorted by the unixtime (descending) when their resell price was last changed.See <a href="https://corefork.telegram.org/api/gifts#sending-gifts">here »</a> for detailed documentation on this method.  
/// Possible errors
/// Code Type Description
/// 400 STARGIFT_INVALID The passed gift is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getResaleStarGifts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetResaleStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetResaleStarGifts, MyTelegram.Schema.Payments.IResaleStarGifts>
{
    protected override Task<MyTelegram.Schema.Payments.IResaleStarGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetResaleStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IResaleStarGifts>(new TResaleStarGifts { Chats = [], Gifts = [], Users = [] });
    }
}