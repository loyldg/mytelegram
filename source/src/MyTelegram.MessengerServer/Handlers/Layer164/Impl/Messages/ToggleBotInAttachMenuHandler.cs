// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ToggleBotInAttachMenuHandler : RpcResultObjectHandler<RequestToggleBotInAttachMenu, IBool>,
    Messages.IToggleBotInAttachMenuHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleBotInAttachMenu obj)
    {
        throw new NotImplementedException();
    }
}