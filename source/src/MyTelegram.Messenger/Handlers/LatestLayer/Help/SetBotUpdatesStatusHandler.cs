namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Informs the server about the number of pending bot updates if they haven't been processed for a long time; for bots only
/// Possible errors
/// Code Type Description
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.setBotUpdatesStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetBotUpdatesStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestSetBotUpdatesStatus, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestSetBotUpdatesStatus obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}