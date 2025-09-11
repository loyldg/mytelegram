namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.toggleViewForumAsMessages" />
///</summary>
internal sealed class ToggleViewForumAsMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = [],
            Chats = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
