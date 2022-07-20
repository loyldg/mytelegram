// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class ToggleJoinToSendHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleJoinToSend, MyTelegram.Schema.IUpdates>,
    Channels.IToggleJoinToSendHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleJoinToSend obj)
    {
        throw new NotImplementedException();
    }
}
