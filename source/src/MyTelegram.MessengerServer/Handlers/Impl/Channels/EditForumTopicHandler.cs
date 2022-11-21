// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class EditForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.IEditForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
