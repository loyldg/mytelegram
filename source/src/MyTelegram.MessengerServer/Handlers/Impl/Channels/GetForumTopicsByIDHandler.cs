// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class GetForumTopicsByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetForumTopicsByID, MyTelegram.Schema.Messages.IForumTopics>,
    Channels.IGetForumTopicsByIDHandler
{
    protected override Task<MyTelegram.Schema.Messages.IForumTopics> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetForumTopicsByID obj)
    {
        throw new NotImplementedException();
    }
}
