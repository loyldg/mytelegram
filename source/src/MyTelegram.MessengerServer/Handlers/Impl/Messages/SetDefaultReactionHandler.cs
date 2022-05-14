// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SetDefaultReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultReaction, IBool>,
    Messages.ISetDefaultReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetDefaultReaction obj)
    {
        throw new NotImplementedException();
    }
}
