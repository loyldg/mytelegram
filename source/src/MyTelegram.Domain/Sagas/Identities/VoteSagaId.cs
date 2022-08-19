namespace MyTelegram.Domain.Sagas;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<VoteSagaId>))]
public class VoteSagaId : SingleValueObject<string>, ISagaId
{
    public VoteSagaId(string value) : base(value)
    {
    }
}