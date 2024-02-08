// ReSharper disable All

namespace MyTelegram.Handlers.Langpack.LayerN;

///<summary>
/// Get information about all languages in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguages" />
///</summary>
internal sealed class GetLanguagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.LayerN.RequestGetLanguages, TVector<ILangPackLanguage>>,
    Langpack.LayerN.IGetLanguagesHandler
{
    protected override Task<TVector<ILangPackLanguage>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.LayerN.RequestGetLanguages obj)
    {
        var r = new TVector<ILangPackLanguage>();
        r.Add(new TLangPackLanguage
        {
            Official = true,
            Name = "English",
            NativeName = "English",
            LangCode = "en",
            PluralCode = "en",
            StringsCount = 2238,
            TranslatedCount = 2238,
            TranslationsUrl = "https://translations.telegram.org/en/"
        });

        r.Add(new TLangPackLanguage
        {
            Official = false,
            Name = "Chinese (Simplified)",
            NativeName = "简体中文",
            LangCode = "zh-hans",
            PluralCode = "zh",
            StringsCount = 2236,
            TranslatedCount = 2229,
            TranslationsUrl = "https://translations.telegram.org/zh-hans/"
        });
        return Task.FromResult(r);
    }
}
