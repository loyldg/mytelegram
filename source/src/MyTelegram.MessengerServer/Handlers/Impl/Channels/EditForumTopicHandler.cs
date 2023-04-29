// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class EditForumTopicHandler : RpcResultObjectHandler<RequestEditForumTopic, Schema.IUpdates>,
    Channels.IEditForumTopicHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
