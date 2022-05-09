namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ForwardMessageSagaId>))]
public class ForwardMessageSagaId : SingleValueObject<string>, ISagaId
{
    public ForwardMessageSagaId(string value) : base(value)
    {
    }
}
