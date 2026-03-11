namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Press an inline callback button and get a callback answer from the bot
/// Possible errors
/// Code Type Description
/// 400 BOT_RESPONSE_TIMEOUT A timeout occurred while fetching data from the bot.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 DATA_INVALID Encrypted data invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PASSWORD_MISSING You must <a href="https://corefork.telegram.org/api/srp">enable 2FA</a> before executing this operation.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// -503 Timeout Timeout while fetching data.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getBotCallbackAnswer"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetBotCallbackAnswerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetBotCallbackAnswer, MyTelegram.Schema.Messages.IBotCallbackAnswer>
{
    protected override Task<MyTelegram.Schema.Messages.IBotCallbackAnswer> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetBotCallbackAnswer obj)
    {
        throw new NotImplementedException();
    }
}