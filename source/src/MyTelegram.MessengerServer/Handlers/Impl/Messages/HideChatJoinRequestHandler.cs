// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class HideChatJoinRequestHandler : RpcResultObjectHandler<RequestHideChatJoinRequest, Schema.IUpdates>,
    Messages.IHideChatJoinRequestHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestHideChatJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}