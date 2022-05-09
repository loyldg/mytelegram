namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UserSignUpSagaId>))]
public class UserSignUpSagaId : SingleValueObject<string>, ISagaId
{
    public UserSignUpSagaId(string value) : base(value)
    {
    }
}
