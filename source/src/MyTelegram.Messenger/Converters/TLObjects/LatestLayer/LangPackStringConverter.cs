using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.Messenger.Converters.TLObjects.LatestLayer;
internal sealed class LangPackStringConverter(IObjectMapper objectMapper)
    : ILangPackStringConverter, ITransientDependency
{
    public int Layer => Layers.LayerLatest;

    public int RequestLayer { get; set; }

    public ILangPackString ToLangPackString(LanguageTextItem item)
    {
        if (!string.IsNullOrEmpty(item.Value))
        {
            // Single value -> TLangPackString
            return new TLangPackString
            {
                Key = item.Key,
                Value = item.Value
            };
        }

        if (!string.IsNullOrEmpty(item.OtherValue))
        {
            // Pluralized values -> TLangPackStringPluralized
            return new TLangPackStringPluralized
            {
                Key = item.Key,
                ZeroValue = item.ZeroValue,
                OneValue = item.OneValue,
                TwoValue = item.TwoValue,
                FewValue = item.FewValue,
                ManyValue = item.ManyValue,
                OtherValue = item.OtherValue ?? string.Empty
            };
        }

        // Deleted string -> TLangPackStringDeleted
        return new TLangPackStringDeleted
        {
            Key = item.Key
        };
    }

    public TVector<ILangPackString> ToLangPackStrings(IReadOnlyCollection<LanguageTextItem> items)
    {
        var vector = new TVector<ILangPackString>();

        foreach (var item in items)
        {
            vector.Add(ToLangPackString(item));
        }

        return vector;
    }
}
