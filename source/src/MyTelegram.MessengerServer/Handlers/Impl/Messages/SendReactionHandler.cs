// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SendReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendReaction, MyTelegram.Schema.IUpdates>,
    Messages.ISendReactionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendReaction obj)
    {
        throw new NotImplementedException();
    }
}
