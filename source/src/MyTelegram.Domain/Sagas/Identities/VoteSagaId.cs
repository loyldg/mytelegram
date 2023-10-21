namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<VoteSagaId>))]
public class VoteSagaId : SingleValueObject<string>, ISagaId
{
    public VoteSagaId(string value) : base(value)
    {
    }
}
