// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SendReactionHandler : RpcResultObjectHandler<RequestSendReaction, Schema.IUpdates>,
    Messages.ISendReactionHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendReaction obj)
    {
        throw new NotImplementedException();
    }
}