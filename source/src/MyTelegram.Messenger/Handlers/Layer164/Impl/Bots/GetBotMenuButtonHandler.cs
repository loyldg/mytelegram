// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Gets the menu button action for a given user or for all users, previously set using <a href="https://corefork.telegram.org/method/bots.setBotMenuButton">bots.setBotMenuButton</a>; users can see this information in the <a href="https://corefork.telegram.org/constructor/botInfo">botInfo</a> constructor.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/bots.getBotMenuButton" />
///</summary>
internal sealed class GetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotMenuButton, MyTelegram.Schema.IBotMenuButton>,
    Bots.IGetBotMenuButtonHandler
{
    protected override Task<MyTelegram.Schema.IBotMenuButton> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}
