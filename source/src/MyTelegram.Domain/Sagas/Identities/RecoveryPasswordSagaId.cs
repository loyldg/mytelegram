namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<RecoveryPasswordSagaId>))]
public class RecoveryPasswordSagaId : Identity<RecoveryPasswordSagaId>, ISagaId
{
    public RecoveryPasswordSagaId(string value) : base(value)
    {
    }
}
