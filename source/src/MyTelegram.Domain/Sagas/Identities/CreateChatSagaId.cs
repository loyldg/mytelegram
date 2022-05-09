namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<CreateChatSagaId>))]
public class CreateChatSagaId : SingleValueObject<string>, ISagaId
{
    public CreateChatSagaId(string value) : base(value)
    {
    }
}
