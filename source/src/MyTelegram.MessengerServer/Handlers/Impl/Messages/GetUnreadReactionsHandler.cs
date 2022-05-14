// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetUnreadReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetUnreadReactions, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetUnreadReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetUnreadReactions obj)
    {
        throw new NotImplementedException();
    }
}
