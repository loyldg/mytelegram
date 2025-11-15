namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Sets the <a href="https://corefork.telegram.org/api/bots/menu">menu button action »</a> for a given user or for all users
/// Possible errors
/// Code Type Description
/// 400 BUTTON_INVALID The specified button is invalid.
/// 400 BUTTON_TEXT_INVALID The specified button text is invalid.
/// 400 BUTTON_URL_INVALID Button URL invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.setBotMenuButton"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotMenuButton, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestSetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}