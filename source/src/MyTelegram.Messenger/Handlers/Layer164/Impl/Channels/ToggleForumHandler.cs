// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/forum">forum functionality</a> in a supergroup.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_DISCUSSION_UNALLOWED You can't enable forum topics in a discussion group linked to a channel.
/// See <a href="https://corefork.telegram.org/method/channels.toggleForum" />
///</summary>
internal sealed class ToggleForumHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleForum, MyTelegram.Schema.IUpdates>,
    Channels.IToggleForumHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleForum obj)
    {
        throw new NotImplementedException();
    }
}
