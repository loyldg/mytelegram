namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.toggleAutotranslation" />
///</summary>
internal sealed class ToggleAutotranslationHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAutotranslation, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleAutotranslation obj)
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
