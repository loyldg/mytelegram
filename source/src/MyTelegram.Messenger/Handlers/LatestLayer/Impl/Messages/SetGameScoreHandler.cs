// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Use this method to set the score of the specified user in a game sent as a normal message (bots only).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_SCORE_NOT_MODIFIED The score wasn't modified.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SCORE_INVALID The specified game score is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.setGameScore" />
///</summary>
internal sealed class SetGameScoreHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetGameScore, MyTelegram.Schema.IUpdates>,
    Messages.ISetGameScoreHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetGameScore obj)
    {
        throw new NotImplementedException();
    }
}
