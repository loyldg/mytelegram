using MyTelegram.Handlers.Langpack;
using MyTelegram.Schema.Langpack;

namespace MyTelegram.MessengerServer.Handlers.Impl.Langpack;

public class GetLanguageHandler : RpcResultObjectHandler<RequestGetLanguage, ILangPackLanguage>,
    IGetLanguageHandler, IProcessedHandler
{
    protected override Task<ILangPackLanguage> HandleCoreAsync(IRequestInput input,
        RequestGetLanguage obj)
    {
        if (obj.LangCode == "zhcncc")
        {
            ILangPackLanguage r0 = new TLangPackLanguage
            {
                //BaseLangCode,
                Official = false,
                Name = "Chinese (Simplified)",
                NativeName = "简体中文",
                BaseLangCode = "zh-hans-raw",
                LangCode = "zhcncc",
                PluralCode = "zh",
                StringsCount = 2236,
                TranslatedCount = 2229,
                TranslationsUrl = "https://translations.telegram.org/zhcncc/"
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