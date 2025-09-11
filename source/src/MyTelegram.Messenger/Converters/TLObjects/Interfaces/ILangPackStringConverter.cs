namespace MyTelegram.Messenger.Converters.TLObjects.Interfaces;
public interface ILangPackStringConverter : ILayeredConverter
{
    ILangPackString ToLangPackString(LanguageTextItem item);
    TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items);
}