namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<InviteToChannelSagaId>))]
public class InviteToChannelSagaId : SingleValueObject<string>, ISagaId
{
    public InviteToChannelSagaId(string value) : base(value)
    {
    }
}
