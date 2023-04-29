// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class UpdatePinnedForumTopicHandler : RpcResultObjectHandler<RequestUpdatePinnedForumTopic, Schema.IUpdates>,
    Channels.IUpdatePinnedForumTopicHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestUpdatePinnedForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
