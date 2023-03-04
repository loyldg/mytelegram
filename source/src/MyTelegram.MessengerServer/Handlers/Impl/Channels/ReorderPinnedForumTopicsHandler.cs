// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

internal sealed class ReorderPinnedForumTopicsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReorderPinnedForumTopics, MyTelegram.Schema.IUpdates>,
    Channels.IReorderPinnedForumTopicsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReorderPinnedForumTopics obj)
    {
        throw new NotImplementedException();
    }
}
