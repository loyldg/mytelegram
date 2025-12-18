namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Allow the specified bot to send us messages
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.allowSendMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AllowSendMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestAllowSendMessage, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestAllowSendMessage obj)
    {
        throw new NotImplementedException();
    }
}