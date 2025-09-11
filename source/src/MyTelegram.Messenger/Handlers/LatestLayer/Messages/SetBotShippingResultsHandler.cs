namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// If you sent an invoice requesting a shipping address and the parameter is_flexible was specified, the bot will receive an <a href="https://corefork.telegram.org/constructor/updateBotShippingQuery">updateBotShippingQuery</a> update. Use this method to reply to shipping queries.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.setBotShippingResults" />
///</summary>
internal sealed class SetBotShippingResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetBotShippingResults, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetBotShippingResults obj)
    {
        throw new NotImplementedException();
    }
}
