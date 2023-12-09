// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Query an inline bot
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INLINE_DISABLED This bot can't be used in inline mode.
/// 400 BOT_INVALID This is not a valid bot.
/// 400 BOT_RESPONSE_TIMEOUT A timeout occurred while fetching data from the bot.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// -503 Timeout Timeout while fetching data.
/// See <a href="https://corefork.telegram.org/method/messages.getInlineBotResults" />
///</summary>
internal sealed class GetInlineBotResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetInlineBotResults, MyTelegram.Schema.Messages.IBotResults>,
    Messages.IGetInlineBotResultsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IBotResults> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetInlineBotResults obj)
    {
        throw new NotImplementedException();
    }
}
