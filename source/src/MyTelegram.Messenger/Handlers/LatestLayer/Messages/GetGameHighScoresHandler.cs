namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get highscores of a game
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getGameHighScores"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetGameHighScoresHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetGameHighScores, MyTelegram.Schema.Messages.IHighScores>
{
    protected override Task<MyTelegram.Schema.Messages.IHighScores> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetGameHighScores obj)
    {
        throw new NotImplementedException();
    }
}