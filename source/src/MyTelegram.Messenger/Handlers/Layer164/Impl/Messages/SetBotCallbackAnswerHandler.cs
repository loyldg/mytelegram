// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Set the callback answer to a user button press (bots only)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_TOO_LONG The provided message is too long.
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 400 URL_INVALID Invalid URL provided.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.setBotCallbackAnswer" />
///</summary>
internal sealed class SetBotCallbackAnswerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetBotCallbackAnswer, IBool>,
    Messages.ISetBotCallbackAnswerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetBotCallbackAnswer obj)
    {
        throw new NotImplementedException();
    }
}
