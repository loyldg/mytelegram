using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getResaleStarGifts" />
///</summary>
internal sealed class GetResaleStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetResaleStarGifts, MyTelegram.Schema.Payments.IResaleStarGifts>
{
    protected override Task<MyTelegram.Schema.Payments.IResaleStarGifts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetResaleStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.IResaleStarGifts>(new TResaleStarGifts
        {
            Chats = [],
            Gifts = [],
            Users = []
        });
    }
}
