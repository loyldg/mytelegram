// ReSharper disable All

using MyTelegram.Schema.Messages;
namespace MyTelegram.Handlers.Messages;

public class GetAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAvailableReactions, MyTelegram.Schema.Messages.IAvailableReactions>,
    Messages.IGetAvailableReactionsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAvailableReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAvailableReactions obj)
    {
        return Task.FromResult<IAvailableReactions>(new TAvailableReactions
        {
            Reactions = new TVector<IAvailableReaction>()
        });
    }
}
