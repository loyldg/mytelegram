namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<AddChatUserSagaId>))]
public class AddChatUserSagaId : SingleValueObject<string>, ISagaId
{
    public AddChatUserSagaId(string value) : base(value)
    {
    }
}
