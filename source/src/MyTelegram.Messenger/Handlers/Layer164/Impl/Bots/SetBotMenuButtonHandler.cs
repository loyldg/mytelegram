// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Sets the <a href="https://corefork.telegram.org/api/bots/menu">menu button action »</a> for a given user or for all users
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BUTTON_TEXT_INVALID The specified button text is invalid.
/// 400 BUTTON_URL_INVALID Button URL invalid.
/// See <a href="https://corefork.telegram.org/method/bots.setBotMenuButton" />
///</summary>
internal sealed class SetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotMenuButton, IBool>,
    Bots.ISetBotMenuButtonHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}
