// ReSharper disable All

using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Channels;

public class DeleteTopicHistoryHandler : RpcResultObjectHandler<RequestDeleteTopicHistory, IAffectedHistory>,
    Channels.IDeleteTopicHistoryHandler
{
    protected override Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        RequestDeleteTopicHistory obj)
    {
        throw new NotImplementedException();
    }
}
