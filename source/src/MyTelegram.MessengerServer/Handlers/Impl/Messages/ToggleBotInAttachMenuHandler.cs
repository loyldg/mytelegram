// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ToggleBotInAttachMenuHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu, IBool>,
    Messages.IToggleBotInAttachMenuHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu obj)
    {
        throw new NotImplementedException();
    }
}
