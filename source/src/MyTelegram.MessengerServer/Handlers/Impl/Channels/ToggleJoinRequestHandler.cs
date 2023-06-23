// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class ToggleJoinRequestHandler : RpcResultObjectHandler<RequestToggleJoinRequest, Schema.IUpdates>,
    Channels.IToggleJoinRequestHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleJoinRequest obj)
    {
        throw new NotImplementedException();
    }
}