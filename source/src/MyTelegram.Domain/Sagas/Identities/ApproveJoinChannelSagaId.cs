namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ApproveJoinChannelSagaId>))]
public class ApproveJoinChannelSagaId : SingleValueObject<string>, ISagaId
{
    public ApproveJoinChannelSagaId(string value) : base(value)
    {
    }
}