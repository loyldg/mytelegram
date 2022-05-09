namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ReadChannelHistorySagaId>))]
public class ReadChannelHistorySagaId : SingleValueObject<string>, ISagaId
{
    public ReadChannelHistorySagaId(string value) : base(value)
    {
    }
}
