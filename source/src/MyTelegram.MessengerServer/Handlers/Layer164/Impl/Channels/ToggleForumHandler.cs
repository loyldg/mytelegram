// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class ToggleForumHandler : RpcResultObjectHandler<RequestToggleForum, Schema.IUpdates>,
    Channels.IToggleForumHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleForum obj)
    {
        throw new NotImplementedException();
    }
}