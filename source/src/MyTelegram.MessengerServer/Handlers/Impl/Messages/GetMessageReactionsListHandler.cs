// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetMessageReactionsListHandler :
    RpcResultObjectHandler<RequestGetMessageReactionsList, IMessageReactionsList>,
    Messages.IGetMessageReactionsListHandler
{
    protected override Task<IMessageReactionsList> HandleCoreAsync(IRequestInput input,
        RequestGetMessageReactionsList obj)
    {
        throw new NotImplementedException();
    }
}
