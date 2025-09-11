namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.updateStarGiftPrice" />
///</summary>
internal sealed class UpdateStarGiftPriceHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestUpdateStarGiftPrice, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestUpdateStarGiftPrice obj)
    {
        return Task.FromResult<MyTelegram.Schema.IUpdates>(new TUpdates
        {
            Chats = [],
            Updates = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
