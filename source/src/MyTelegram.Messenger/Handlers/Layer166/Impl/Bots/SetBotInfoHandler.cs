// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Set localized name, about text and description of a bot (or of the current account, if called by a bot).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// See <a href="https://corefork.telegram.org/method/bots.setBotInfo" />
///</summary>
internal sealed class SetBotInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotInfo, IBool>,
    Bots.ISetBotInfoHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotInfo obj)
    {
        throw new NotImplementedException();
    }
}
