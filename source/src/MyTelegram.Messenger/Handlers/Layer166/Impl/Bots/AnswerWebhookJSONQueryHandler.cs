// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Answers a custom query; for bots only
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 403 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// See <a href="https://corefork.telegram.org/method/bots.answerWebhookJSONQuery" />
///</summary>
internal sealed class AnswerWebhookJSONQueryHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestAnswerWebhookJSONQuery, IBool>,
    Bots.IAnswerWebhookJSONQueryHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestAnswerWebhookJSONQuery obj)
    {
        throw new NotImplementedException();
    }
}
