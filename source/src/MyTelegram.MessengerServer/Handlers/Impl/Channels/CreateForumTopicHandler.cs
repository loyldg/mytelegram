// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class CreateForumTopicHandler : RpcResultObjectHandler<RequestCreateForumTopic, Schema.IUpdates>,
    Channels.ICreateForumTopicHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestCreateForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
