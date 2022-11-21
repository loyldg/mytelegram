// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class GetForumTopicsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetForumTopics, MyTelegram.Schema.Messages.IForumTopics>,
    Channels.IGetForumTopicsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IForumTopics> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetForumTopics obj)
    {
        throw new NotImplementedException();
    }
}
