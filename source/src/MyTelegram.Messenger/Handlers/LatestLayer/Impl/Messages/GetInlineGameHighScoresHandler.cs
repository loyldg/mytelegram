// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get highscores of a game sent using an inline bot
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.getInlineGameHighScores" />
///</summary>
internal sealed class GetInlineGameHighScoresHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetInlineGameHighScores, MyTelegram.Schema.Messages.IHighScores>,
    Messages.IGetInlineGameHighScoresHandler
{
    protected override Task<MyTelegram.Schema.Messages.IHighScores> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetInlineGameHighScores obj)
    {
        throw new NotImplementedException();
    }
}
