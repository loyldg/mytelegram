// ReSharper disable All

using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Channels;

public class GetForumTopicsByIDHandler : RpcResultObjectHandler<RequestGetForumTopicsByID, IForumTopics>,
    Channels.IGetForumTopicsByIDHandler
{
    protected override Task<IForumTopics> HandleCoreAsync(IRequestInput input,
        RequestGetForumTopicsByID obj)
    {
        throw new NotImplementedException();
    }
}
