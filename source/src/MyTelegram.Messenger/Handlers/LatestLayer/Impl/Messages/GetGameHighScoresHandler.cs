// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get highscores of a game
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.getGameHighScores" />
///</summary>
internal sealed class GetGameHighScoresHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetGameHighScores, MyTelegram.Schema.Messages.IHighScores>,
    Messages.IGetGameHighScoresHandler
{
    protected override Task<MyTelegram.Schema.Messages.IHighScores> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetGameHighScores obj)
    {
        throw new NotImplementedException();
    }
}
