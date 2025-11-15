namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Users may also choose to display messages from all topics of a <a href="https://corefork.telegram.org/api/forum">forum</a> as if they were sent to a normal group, using a "View as messages" setting in the local client: this setting only affects the current account, and is synced to other logged in sessions using this method.Invoking this method will update the value of the <code>view_forum_as_messages</code> flag of <a href="https://corefork.telegram.org/constructor/channelFull">channelFull</a> or <a href="https://corefork.telegram.org/constructor/dialog">dialog</a> and emit an <a href="https://corefork.telegram.org/constructor/updateChannelViewForumAsMessages">updateChannelViewForumAsMessages</a>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.toggleViewForumAsMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleViewForumAsMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestToggleViewForumAsMessages obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}