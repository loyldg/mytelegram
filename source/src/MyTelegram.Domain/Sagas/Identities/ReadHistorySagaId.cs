namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ReadHistorySagaId>))]
public class ReadHistorySagaId : SingleValueObject<string>, ISagaId
{
    public ReadHistorySagaId(string value) : base(value)
    {
    }
}
