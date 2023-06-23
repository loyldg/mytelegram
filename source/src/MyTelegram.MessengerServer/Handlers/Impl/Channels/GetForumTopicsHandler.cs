// ReSharper disable All

using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Channels;

public class GetForumTopicsHandler : RpcResultObjectHandler<RequestGetForumTopics, IForumTopics>,
    Channels.IGetForumTopicsHandler
{
    protected override Task<IForumTopics> HandleCoreAsync(IRequestInput input,
        RequestGetForumTopics obj)
    {
        throw new NotImplementedException();
    }
}