// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Obtain a list of bot commands for the specified bot scope and language code
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// See <a href="https://corefork.telegram.org/method/bots.getBotCommands" />
///</summary>
internal sealed class GetBotCommandsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotCommands, TVector<MyTelegram.Schema.IBotCommand>>,
    Bots.IGetBotCommandsHandler
{
    protected override Task<TVector<MyTelegram.Schema.IBotCommand>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}
