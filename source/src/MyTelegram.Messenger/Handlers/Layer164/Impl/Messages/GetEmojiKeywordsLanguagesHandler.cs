// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Obtain a list of related languages that must be used when fetching <a href="https://corefork.telegram.org/api/custom-emoji#emoji-keywords">emoji keyword lists »</a>.Usually the method will return the passed language codes (if localized) + <code>en</code> + some language codes for similar languages (if applicable).
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiKeywordsLanguages" />
///</summary>
internal sealed class GetEmojiKeywordsLanguagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiKeywordsLanguages, TVector<MyTelegram.Schema.IEmojiLanguage>>,
    Messages.IGetEmojiKeywordsLanguagesHandler
{
    protected override Task<TVector<MyTelegram.Schema.IEmojiLanguage>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiKeywordsLanguages obj)
    {
        var languages = new[] { new TEmojiLanguage { LangCode = "en" }, new TEmojiLanguage { LangCode = "zh-hans" } };

        var r = new TVector<IEmojiLanguage>(languages);

        return Task.FromResult(r);
    }
}
