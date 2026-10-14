namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface ILangPackStringConverterService
{
    ILangPackString ToLangPackString(LanguageTextItem item);
    TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items);
}