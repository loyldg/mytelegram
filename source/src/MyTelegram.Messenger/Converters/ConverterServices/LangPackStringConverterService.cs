namespace MyTelegram.Messenger.Converters.ConverterServices;

public class LangPackStringConverterService(ILayeredService<ILangPackStringConverter> langPackStringLayeredService) : ILangPackStringConverterService, ITransientDependency
{
    public ILangPackString ToLangPackString(LanguageTextItem item, int layer)
    {
        return langPackStringLayeredService.GetConverter(layer).ToLangPackString(item);
    }

    public TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items, int layer)
    {
        return langPackStringLayeredService.GetConverter(layer).ToLangPackStrings(items);
    }
}
