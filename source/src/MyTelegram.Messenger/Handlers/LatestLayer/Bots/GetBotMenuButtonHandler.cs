namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Gets the menu button action for a given user or for all users, previously set using <a href="https://corefork.telegram.org/method/bots.setBotMenuButton">bots.setBotMenuButton</a>; users can see this information in the <a href="https://corefork.telegram.org/constructor/botInfo">botInfo</a> constructor.
/// Possible errors
/// Code Type Description
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.getBotMenuButton"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotMenuButton, MyTelegram.Schema.IBotMenuButton>
{
    protected override Task<MyTelegram.Schema.IBotMenuButton> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}