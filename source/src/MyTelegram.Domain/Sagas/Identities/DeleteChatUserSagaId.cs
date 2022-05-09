namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteChatUserSagaId>))]
public class DeleteChatUserSagaId : SingleValueObject<string>, ISagaId
{
    public DeleteChatUserSagaId(string value) : base(value)
    {
    }
}
