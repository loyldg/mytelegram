// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.toggleViewForumAsMessages" />
///</summary>
internal sealed class ToggleViewForumAsMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages, MyTelegram.Schema.IUpdates>,
    Channels.IToggleViewForumAsMessagesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages obj)
    {
        throw new NotImplementedException();
    }
}
