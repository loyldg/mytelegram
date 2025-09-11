using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Get a list of available <a href="https://corefork.telegram.org/api/gifts">gifts, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/payments.getStarGifts" />
///</summary>
internal sealed class GetStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGifts, MyTelegram.Schema.Payments.IStarGifts>
{
    protected override Task<MyTelegram.Schema.Payments.IStarGifts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IStarGifts>(new TStarGifts
        {
            Gifts = [],
            Chats = [],
            Users = []
        });
    }
}
