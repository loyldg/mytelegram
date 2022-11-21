// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class CreateForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCreateForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.ICreateForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestCreateForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
