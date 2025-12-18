namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Check whether the specified bot can send us messages
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.canSendMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CanSendMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestCanSendMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestCanSendMessage obj)
    {
        return System.Threading.Tasks.Task.FromResult<IBool>(new TBoolTrue());
    }
}