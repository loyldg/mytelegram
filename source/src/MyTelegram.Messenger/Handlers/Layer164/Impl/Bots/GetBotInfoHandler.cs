// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Get localized name, about text and description of a bot (or of the current account, if called by a bot).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_INVALID The specified language code is invalid.
/// 400 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// See <a href="https://corefork.telegram.org/method/bots.getBotInfo" />
///</summary>
internal sealed class GetBotInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotInfo, MyTelegram.Schema.Bots.IBotInfo>,
    Bots.IGetBotInfoHandler
{
    protected override Task<MyTelegram.Schema.Bots.IBotInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetBotInfo obj)
    {
        throw new NotImplementedException();
    }
}
