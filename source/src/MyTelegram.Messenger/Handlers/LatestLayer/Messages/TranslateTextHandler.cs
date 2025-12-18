namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Translate a given text.<a href="https://corefork.telegram.org/api/entities">Styled text entities</a> will only be preserved for <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> users.
/// Possible errors
/// Code Type Description
/// 400 INPUT_TEXT_EMPTY The specified text is empty.
/// 400 INPUT_TEXT_TOO_LONG The specified text is too long.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TO_LANG_INVALID The specified destination language is invalid.
/// 500 TRANSLATE_REQ_FAILED Translation failed, please try again later.
/// 400 TRANSLATE_REQ_QUOTA_EXCEEDED Translation is currently unavailable due to a temporary server-side lack of resources.
/// 406 TRANSLATIONS_DISABLED Translations are unavailable, a detailed and localized description for the error will be emitted via an <a href="https://corefork.telegram.org/api/errors#406-not-acceptable">updateServiceNotification as specified here »</a>.
/// 500 TRANSLATION_TIMEOUT A timeout occurred while translating the specified text.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.translateText"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TranslateTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranslateText, MyTelegram.Schema.Messages.ITranslatedText>
{
    protected override Task<MyTelegram.Schema.Messages.ITranslatedText> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestTranslateText obj)
    {
        var r = new TTranslateResult
        {
            Result = [..obj.Id?.Select(p => new TTextWithEntities { Entities = [], Text = $"The external translation API is not configured for MyTelegram, and the text will not be translated, ToLang: {obj.ToLang}" }).ToList() ?? []]
        };
        return Task.FromResult<ITranslatedText>(r);
    }
}