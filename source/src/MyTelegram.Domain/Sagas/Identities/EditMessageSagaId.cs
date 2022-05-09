namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditMessageSagaId>))]
public class EditMessageSagaId : SingleValueObject<string>, ISagaId
{
    public EditMessageSagaId(string value) : base(value)
    {
    }
}
