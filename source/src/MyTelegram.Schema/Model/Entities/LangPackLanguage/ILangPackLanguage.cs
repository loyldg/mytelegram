// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILangPackLanguage : IObject
{
    BitArray Flags { get; set; }
    bool Official { get; set; }
    bool Rtl { get; set; }
    bool Beta { get; set; }
    string Name { get; set; }
    string NativeName { get; set; }
    string LangCode { get; set; }
    string? BaseLangCode { get; set; }
    string PluralCode { get; set; }
    int StringsCount { get; set; }
    int TranslatedCount { get; set; }
    string TranslationsUrl { get; set; }

}
