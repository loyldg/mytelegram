// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Once the user has confirmed their payment and shipping details, the bot receives an <a href="https://corefork.telegram.org/constructor/updateBotPrecheckoutQuery">updateBotPrecheckoutQuery</a> update.<br>
/// Use this method to respond to such pre-checkout queries.<br>
/// <strong>Note</strong>: Telegram must receive an answer within 10 seconds after the pre-checkout query was sent.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ERROR_TEXT_EMPTY The provided error message is empty.
/// See <a href="https://corefork.telegram.org/method/messages.setBotPrecheckoutResults" />
///</summary>
internal sealed class SetBotPrecheckoutResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetBotPrecheckoutResults, IBool>,
    Messages.ISetBotPrecheckoutResultsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetBotPrecheckoutResults obj)
    {
        throw new NotImplementedException();
    }
}
