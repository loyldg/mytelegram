// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get information about a language in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguage" />
///</summary>
internal sealed class GetLanguageHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetLanguage, MyTelegram.Schema.ILangPackLanguage>,
    Langpack.IGetLanguageHandler
{
    protected override Task<ILangPackLanguage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetLanguage obj)
    {
        if (obj.LangCode == "zh-hans")
        {
            ILangPackLanguage r0 = new TLangPackLanguage
            {
                //BaseLangCode,
                Official = false,
                Name = "Chinese (Simplified)",
                NativeName = "简体中文",
                BaseLangCode = "zh-hans",
                LangCode = "zh-hans",
                PluralCode = "zh",
                StringsCount = 2236,
                TranslatedCount = 2229,
                TranslationsUrl = "https://translations.telegram.org/zh-hans/"
            };
            return Task.FromResult(r0);
        }

        ILangPackLanguage r = new TLangPackLanguage
        {
            //BaseLangCode,
            LangCode = obj.LangCode,
            Name = "English",
            NativeName = "English",
            PluralCode = "en",
            Official = false,
            Beta = false,
            Rtl = false,
            StringsCount = 0,
            TranslatedCount = 0,
            TranslationsUrl = "https://translations.telegram.org/en/"
        };

        return Task.FromResult(r);
    }
}
