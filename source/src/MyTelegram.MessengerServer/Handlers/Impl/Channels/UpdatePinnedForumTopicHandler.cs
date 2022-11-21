// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class UpdatePinnedForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdatePinnedForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.IUpdatePinnedForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdatePinnedForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
