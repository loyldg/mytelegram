namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface ILangPackStringConverterService
{
    ILangPackString ToLangPackString(LanguageTextItem item, int layer);
    TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items, int layer);
}