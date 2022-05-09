namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UpdatePinnedMessageSagaId>))]
public class UpdatePinnedMessageSagaId : SingleValueObject<string>, ISagaId
{
    public UpdatePinnedMessageSagaId(string value) : base(value)
    {
    }
}
