namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Use this method to set the score of the specified user in a game sent as an inline message (bots only).
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setInlineGameScore"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetInlineGameScoreHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetInlineGameScore, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetInlineGameScore obj)
    {
        throw new NotImplementedException();
    }
}