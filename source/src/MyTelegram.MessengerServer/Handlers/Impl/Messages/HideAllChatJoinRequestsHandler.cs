// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class HideAllChatJoinRequestsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests, MyTelegram.Schema.IUpdates>,
    Messages.IHideAllChatJoinRequestsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests obj)
    {
        throw new NotImplementedException();
    }
}
