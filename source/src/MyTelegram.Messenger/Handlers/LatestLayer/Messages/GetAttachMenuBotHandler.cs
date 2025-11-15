namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns attachment menu entry for a <a href="https://corefork.telegram.org/api/bots/attach">bot mini app that can be launched from the attachment menu »</a>
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getAttachMenuBot"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAttachMenuBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBot, MyTelegram.Schema.IAttachMenuBotsBot>
{
    protected override Task<MyTelegram.Schema.IAttachMenuBotsBot> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetAttachMenuBot obj)
    {
        throw new NotImplementedException();
    }
}