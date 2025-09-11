namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Once the user has confirmed their payment and shipping details, the bot receives an <a href="https://corefork.telegram.org/constructor/updateBotPrecheckoutQuery">updateBotPrecheckoutQuery</a> update.<br>
/// Use this method to respond to such pre-checkout queries.<br>
/// <strong>Note</strong>: Telegram must receive an answer within 10 seconds after the pre-checkout query was sent.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ERROR_TEXT_EMPTY The provided error message is empty.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.setBotPrecheckoutResults" />
///</summary>
internal sealed class SetBotPrecheckoutResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetBotPrecheckoutResults, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetBotPrecheckoutResults obj)
    {
        throw new NotImplementedException();
    }
}
