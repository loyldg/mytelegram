// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetAvailableReactionsHandler : RpcResultObjectHandler<Schema.Messages.RequestGetAvailableReactions,
        Schema.Messages.IAvailableReactions>,
    Messages.IGetAvailableReactionsHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IAvailableReactions> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetAvailableReactions obj)
    {
        return Task.FromResult<IAvailableReactions>(new TAvailableReactions
        {
            Reactions = new TVector<IAvailableReaction>()
        });
    }
}