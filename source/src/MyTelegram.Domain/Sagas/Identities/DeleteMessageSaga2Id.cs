namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteMessageSaga2Id>))]
public class DeleteMessageSaga2Id : SingleValueObject<string>, ISagaId
{
    public DeleteMessageSaga2Id(string value) : base(value)
    {
    }
}
