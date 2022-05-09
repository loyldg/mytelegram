namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteMessageSagaId>))]
public class DeleteMessageSagaId : SingleValueObject<string>, ISagaId
{
    public DeleteMessageSagaId(string value) : base(value)
    {
    }
}
