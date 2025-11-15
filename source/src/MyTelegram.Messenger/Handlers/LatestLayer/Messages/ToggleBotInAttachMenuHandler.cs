namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/bots/attach">web bot attachment menu »</a>
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleBotInAttachMenu"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleBotInAttachMenuHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestToggleBotInAttachMenu obj)
    {
        throw new NotImplementedException();
    }
}