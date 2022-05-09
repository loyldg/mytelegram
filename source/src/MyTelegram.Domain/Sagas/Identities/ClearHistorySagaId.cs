namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ClearHistorySagaId>))]
public class ClearHistorySagaId : SingleValueObject<string>, ISagaId
{
    public ClearHistorySagaId(string value) : base(value)
    {
    }
}
