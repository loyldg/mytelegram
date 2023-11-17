// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/bots/attach">web bot attachment menu »</a>
/// See <a href="https://corefork.telegram.org/method/messages.toggleBotInAttachMenu" />
///</summary>
internal sealed class ToggleBotInAttachMenuHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu, IBool>,
    Messages.IToggleBotInAttachMenuHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu obj)
    {
        throw new NotImplementedException();
    }
}
