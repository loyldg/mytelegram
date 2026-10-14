namespace MyTelegram.Messenger.Converters.ConverterServices;

public class LangPackStringConverterService(ILangPackStringConverter langPackStringConverter) : ILangPackStringConverterService, ITransientDependency
{
    public ILangPackString ToLangPackString(LanguageTextItem item)
    {
        return langPackStringConverter.ToLangPackString(item);
    }

    public TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items)
    {
        return langPackStringConverter.ToLangPackStrings(items);
    }
}
