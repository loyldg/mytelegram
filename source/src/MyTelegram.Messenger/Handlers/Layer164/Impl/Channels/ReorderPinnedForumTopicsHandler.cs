// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Reorder pinned forum topics
/// See <a href="https://corefork.telegram.org/method/channels.reorderPinnedForumTopics" />
///</summary>
internal sealed class ReorderPinnedForumTopicsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReorderPinnedForumTopics, MyTelegram.Schema.IUpdates>,
    Channels.IReorderPinnedForumTopicsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReorderPinnedForumTopics obj)
    {
        throw new NotImplementedException();
    }
}
