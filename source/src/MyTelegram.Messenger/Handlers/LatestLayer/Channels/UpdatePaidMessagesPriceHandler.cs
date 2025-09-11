namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.updatePaidMessagesPrice" />
///</summary>
internal sealed class UpdatePaidMessagesPriceHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdatePaidMessagesPrice, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdatePaidMessagesPrice obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Chats = [],
            Updates = [],
            Users = []
        });
    }
}
