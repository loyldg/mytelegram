using MyTelegram.Handlers.Langpack;
using MyTelegram.Schema.Langpack;
using MyTelegram.Schema.LayerN;

namespace MyTelegram.MessengerServer.Handlers.Impl.Langpack;

public class GetLanguagesHandler : RpcResultObjectHandler<RequestGetLanguages, TVector<ILangPackLanguage>>,
    IGetLanguagesHandler, IProcessedHandler
{
    protected override Task<TVector<ILangPackLanguage>> HandleCoreAsync(IRequestInput input,
        RequestGetLanguages obj)
    {
        var r = new TVector<ILangPackLanguage>
        {
            new TLangPackLanguage
            {
                Official = true,
                Name = "English",
                NativeName = "English",
                LangCode = "en",
                PluralCode = "en",
                StringsCount = 2238,
                TranslatedCount = 2238,
                TranslationsUrl = "https://translations.telegram.org/en/"
            },
            new TLangPackLanguage
            {
                Official = false,
                Name = "Chinese (Simplified)",
                NativeName = "简体中文",
                LangCode = "zhcncc",
                PluralCode = "zh",
                StringsCount = 2236,
                TranslatedCount = 2229,
                TranslationsUrl = "https://translations.telegram.org/zhcncc/"
            }
        };

        return Task.FromResult(r);
    }
}

public class GetLanguagesHandlerLayerN : RpcResultObjectHandler<RequestGetLanguagesLayerN, TVector<ILangPackLanguage>>,
    IGetLanguagesHandlerLayerN, IProcessedHandler
{
    protected override Task<TVector<ILangPackLanguage>> HandleCoreAsync(IRequestInput input,
        RequestGetLanguagesLayerN obj)
    {
        var r = new TVector<ILangPackLanguage>
        {
            new TLangPackLanguage
            {
                Official = true,
                Name = "English",
                NativeName = "English",
                LangCode = "en",
                PluralCode = "en",
                StringsCount = 2238,
                TranslatedCount = 2238,
                TranslationsUrl = "https://translations.telegram.org/en/"
            },
            new TLangPackLanguage
            {
                Official = false,
                Name = "Chinese (Simplified)",
                NativeName = "简体中文",
                LangCode = "zhcncc",
                PluralCode = "zh",
                StringsCount = 2236,
                TranslatedCount = 2229,
                TranslationsUrl = "https://translations.telegram.org/zhcncc/"
            }
        };

        return Task.FromResult(r);
    }
}
