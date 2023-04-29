// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SetDefaultReactionHandler : RpcResultObjectHandler<RequestSetDefaultReaction, IBool>,
    Messages.ISetDefaultReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetDefaultReaction obj)
    {
        throw new NotImplementedException();
    }
}
