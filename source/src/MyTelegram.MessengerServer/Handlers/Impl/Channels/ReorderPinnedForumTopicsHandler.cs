// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

internal sealed class ReorderPinnedForumTopicsHandler :
    RpcResultObjectHandler<RequestReorderPinnedForumTopics, Schema.IUpdates>,
    Channels.IReorderPinnedForumTopicsHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestReorderPinnedForumTopics obj)
    {
        throw new NotImplementedException();
    }
}
