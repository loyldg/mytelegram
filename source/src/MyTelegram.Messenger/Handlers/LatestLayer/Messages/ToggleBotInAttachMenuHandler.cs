namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/bots/attach">web bot attachment menu »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/messages.toggleBotInAttachMenu" />
///</summary>
internal sealed class ToggleBotInAttachMenuHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu obj)
    {
        throw new NotImplementedException();
    }
}
