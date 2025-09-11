using MyTelegram.Schema.Langpack;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Langpack;

///<summary>
/// Get information about all languages in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguages" />
///</summary>
internal sealed class GetLanguagesHandler(ILanguageCacheService languageCacheService) : RpcResultObjectHandler<RequestGetLanguages,
        TVector<Schema.ILangPackLanguage>>
{
    protected override async Task<TVector<ILangPackLanguage>> HandleCoreAsync(IRequestInput input,
        RequestGetLanguages obj)
    {
        var langPack = obj.LangPack;
        if (string.IsNullOrEmpty(langPack))
        {
            langPack = input.DeviceType.ToString().ToLower();
        }
        var languageReadModels = await languageCacheService.GetAllLanguagesAsync(langPack);
        var languages = new TVector<ILangPackLanguage>();
        foreach (var languageReadModel in languageReadModels)
        {
            var langPackLanguage = new TLangPackLanguage
            {
                Rtl = languageReadModel.Rtl,
                Name = languageReadModel.Name,
                NativeName = languageReadModel.NativeName,
                LangCode = languageReadModel.LanguageCode,
                PluralCode = languageReadModel.LanguageCode,
                StringsCount = languageReadModel.TranslatedCount,
                TranslatedCount = languageReadModel.TranslatedCount,
                TranslationsUrl = languageReadModel.TranslationsUrl
            };
            languages.Add(langPackLanguage);
        }

        return languages;
    }
}