namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.updateEmojiStatus" />
///</summary>
internal sealed class UpdateEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateEmojiStatus, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateEmojiStatus obj)
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
