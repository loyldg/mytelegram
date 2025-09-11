using MyTelegram.Schema.Langpack;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Langpack;

///<summary>
/// Get information about a language in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguage" />
///</summary>
internal sealed class GetLanguageHandler(ILanguageCacheService languageCacheService) : RpcResultObjectHandler<RequestGetLanguage, Schema.ILangPackLanguage>
{
    protected override async Task<ILangPackLanguage> HandleCoreAsync(IRequestInput input,
        RequestGetLanguage obj)
    {
        var languageReadModel = await languageCacheService.GetLanguageAsync(obj.LangCode, obj.LangPack);
        if (languageReadModel == null)
        {
            RpcErrors.RpcErrors400.LangPackInvalid.ThrowRpcError();
        }

        var langPackLanguage = new TLangPackLanguage
        {
            Rtl = languageReadModel!.Rtl,
            Name = languageReadModel.Name,
            NativeName = languageReadModel.NativeName,
            LangCode = languageReadModel.LanguageCode,
            PluralCode = languageReadModel.LanguageCode,
            StringsCount = languageReadModel.TranslatedCount,
            TranslatedCount = languageReadModel.TranslatedCount,
            TranslationsUrl = languageReadModel.TranslationsUrl
        };

        return langPackLanguage;
    }
}