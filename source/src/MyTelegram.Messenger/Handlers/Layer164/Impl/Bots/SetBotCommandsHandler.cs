// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Set bot command list
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_COMMAND_DESCRIPTION_INVALID The specified command description is invalid.
/// 400 BOT_COMMAND_INVALID The specified command is invalid.
/// 400 LANG_CODE_INVALID The specified language code is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/bots.setBotCommands" />
///</summary>
internal sealed class SetBotCommandsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotCommands, IBool>,
    Bots.ISetBotCommandsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}
