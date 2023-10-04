// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class ToggleJoinToSendHandler : RpcResultObjectHandler<RequestToggleJoinToSend, Schema.IUpdates>,
    Channels.IToggleJoinToSendHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleJoinToSend obj)
    {
        throw new NotImplementedException();
    }
}