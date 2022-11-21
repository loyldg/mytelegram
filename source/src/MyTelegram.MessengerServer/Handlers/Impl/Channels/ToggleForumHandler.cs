// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class ToggleForumHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleForum, MyTelegram.Schema.IUpdates>,
    Channels.IToggleForumHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleForum obj)
    {
        throw new NotImplementedException();
    }
}
