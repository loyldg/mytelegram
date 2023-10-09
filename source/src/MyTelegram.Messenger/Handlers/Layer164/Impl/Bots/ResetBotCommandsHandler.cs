// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Clear bot commands for the specified bot scope and language code
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_INVALID The specified language code is invalid.
/// See <a href="https://corefork.telegram.org/method/bots.resetBotCommands" />
///</summary>
internal sealed class ResetBotCommandsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestResetBotCommands, IBool>,
    Bots.IResetBotCommandsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestResetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}
