// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class HideAllChatJoinRequestsHandler : RpcResultObjectHandler<RequestHideAllChatJoinRequests, Schema.IUpdates>,
    Messages.IHideAllChatJoinRequestsHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestHideAllChatJoinRequests obj)
    {
        throw new NotImplementedException();
    }
}
