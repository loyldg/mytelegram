namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<CreateChannelSagaId>))]
public class CreateChannelSagaId : SingleValueObject<string>, ISagaId
{
    public CreateChannelSagaId(string value) : base(value)
    {
    }
}
