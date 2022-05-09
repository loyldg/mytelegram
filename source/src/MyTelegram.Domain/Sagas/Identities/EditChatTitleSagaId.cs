namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChatTitleSagaId>))]
public class EditChatTitleSagaId : SingleValueObject<string>, ISagaId
{
    public EditChatTitleSagaId(string value) : base(value)
    {
    }
}
