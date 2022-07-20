// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class ToggleJoinRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleJoinRequest, MyTelegram.Schema.IUpdates>,
    Channels.IToggleJoinRequestHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}
