// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Start a conversation with a bot using a <a href="https://corefork.telegram.org/api/links#bot-links">deep linking parameter</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 START_PARAM_EMPTY The start parameter is empty.
/// 400 START_PARAM_INVALID Start parameter invalid.
/// 400 START_PARAM_TOO_LONG Start parameter is too long.
/// See <a href="https://corefork.telegram.org/method/messages.startBot" />
///</summary>
internal sealed class StartBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestStartBot, MyTelegram.Schema.IUpdates>,
    Messages.IStartBotHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestStartBot obj)
    {
        throw new NotImplementedException();
    }
}
