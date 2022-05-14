// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class HideChatJoinRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideChatJoinRequest, MyTelegram.Schema.IUpdates>,
    Messages.IHideChatJoinRequestHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideChatJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}
