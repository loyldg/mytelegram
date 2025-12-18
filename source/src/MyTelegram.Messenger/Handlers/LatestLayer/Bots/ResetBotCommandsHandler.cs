namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Clear bot commands for the specified bot scope and language code
/// Possible errors
/// Code Type Description
/// 400 LANG_CODE_INVALID The specified language code is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.resetBotCommands"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ResetBotCommandsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestResetBotCommands, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestResetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}