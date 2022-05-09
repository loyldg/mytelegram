namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<JoinChannelSagaId>))]
public class JoinChannelSagaId : SingleValueObject<string>, ISagaId
{
    public JoinChannelSagaId(string value) : base(value)
    {
    }
}
