// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetMessageReactionsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessageReactionsList, MyTelegram.Schema.Messages.IMessageReactionsList>,
    Messages.IGetMessageReactionsListHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessageReactionsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessageReactionsList obj)
    {
        throw new NotImplementedException();
    }
}
