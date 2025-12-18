namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Obtain a list of bot commands for the specified bot scope and language code
/// Possible errors
/// Code Type Description
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.getBotCommands"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetBotCommandsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotCommands, TVector<MyTelegram.Schema.IBotCommand>>
{
    protected override Task<TVector<MyTelegram.Schema.IBotCommand>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}