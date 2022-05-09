namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ImportContactsSagaId>))]
public class ImportContactsSagaId : SingleValueObject<string>, ISagaId
{
    public ImportContactsSagaId(string value) : base(value)
    {
    }
}
