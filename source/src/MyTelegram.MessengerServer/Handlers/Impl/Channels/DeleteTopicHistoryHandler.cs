// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class DeleteTopicHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteTopicHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Channels.IDeleteTopicHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteTopicHistory obj)
    {
        throw new NotImplementedException();
    }
}
