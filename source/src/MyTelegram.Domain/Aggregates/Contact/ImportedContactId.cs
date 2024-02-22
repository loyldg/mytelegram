namespace MyTelegram.Domain.Aggregates.Contact;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ImportedContactId>))]
public class ImportedContactId : Identity<ImportedContactId>
{
    public ImportedContactId(string value) : base(value)
    {
    }

    public static ImportedContactId Create(long selfUserId,
        string phone)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"importedcontact-{selfUserId}-{phone}");
    }
}
