namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UpdateUserNameSagaId>))]
public class UpdateUserNameSagaId : SingleValueObject<string>, ISagaId
{
    public UpdateUserNameSagaId(string value) : base(value)
    {
    }
}
