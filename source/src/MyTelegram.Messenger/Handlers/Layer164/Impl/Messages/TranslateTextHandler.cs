// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Translate a given text.<a href="https://corefork.telegram.org/api/entities">Styled text entities</a> will only be preserved for <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> users.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INPUT_TEXT_EMPTY The specified text is empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TO_LANG_INVALID The specified destination language is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.translateText" />
///</summary>
internal sealed class TranslateTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranslateText, MyTelegram.Schema.Messages.ITranslatedText>,
    Messages.ITranslateTextHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranslatedText> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTranslateText obj)
    {
        var r = new TTranslateResult
        {
            Result = new TVector<ITextWithEntities>(obj.Id?.Select(p => new TTextWithEntities
            {
                Entities = new(),
                Text =
                    $"The external translation API is not configured for MyTelegram,and the text will not be translated,ToLang:{obj.ToLang}"
            }).ToList() ?? new List<TTextWithEntities>())
        };

        return Task.FromResult<ITranslatedText>(r);
        //return new TTranslateResultText
        //{
        //    Text=$"The external translation API is not configured,ToLang:{obj.ToLang} Original text:{obj.Text}"
        //};
    }
}
