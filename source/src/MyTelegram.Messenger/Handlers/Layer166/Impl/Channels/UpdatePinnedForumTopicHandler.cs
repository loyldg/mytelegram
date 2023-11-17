// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Pin or unpin <a href="https://corefork.telegram.org/api/forum">forum topics</a>
/// See <a href="https://corefork.telegram.org/method/channels.updatePinnedForumTopic" />
///</summary>
internal sealed class UpdatePinnedForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdatePinnedForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.IUpdatePinnedForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdatePinnedForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
