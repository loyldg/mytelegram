namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<SendMessageSagaId>))]
public class SendMessageSagaId : SingleValueObject<string>, ISagaId
{
    public SendMessageSagaId(string value) : base(value)
    {
    }
}