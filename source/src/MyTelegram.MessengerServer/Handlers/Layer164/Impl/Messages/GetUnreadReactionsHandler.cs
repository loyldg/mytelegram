// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetUnreadReactionsHandler : RpcResultObjectHandler<RequestGetUnreadReactions, IMessages>,
    Messages.IGetUnreadReactionsHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetUnreadReactions obj)
    {
        throw new NotImplementedException();
    }
}