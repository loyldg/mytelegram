namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChannelTitleSagaId>))]
public class EditChannelTitleSagaId : SingleValueObject<string>, ISagaId
{
    public EditChannelTitleSagaId(string value) : base(value)
    {
    }
}
