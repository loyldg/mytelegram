namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Save a <a href="https://corefork.telegram.org/api/bots/inline#21-using-a-prepared-inline-message">prepared inline message</a>, to be shared by the user of the mini app using a <a href="https://corefork.telegram.org/api/web-events#web-app-send-prepared-message">web_app_send_prepared_message event</a>
/// Possible errors
/// Code Type Description
/// 400 RESULT_ID_INVALID One of the specified result IDs is invalid.
/// 400 SEND_MESSAGE_GAME_INVALID An inputBotInlineMessageGame can only be contained in an inputBotInlineResultGame, not in an inputBotInlineResult/inputBotInlineResultPhoto/etc.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.savePreparedInlineMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SavePreparedInlineMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSavePreparedInlineMessage, MyTelegram.Schema.Messages.IBotPreparedInlineMessage>
{
    protected override Task<MyTelegram.Schema.Messages.IBotPreparedInlineMessage> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSavePreparedInlineMessage obj)
    {
        throw new NotImplementedException();
    }
}