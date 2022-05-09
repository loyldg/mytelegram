namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<SignInSagaId>))]
public class SignInSagaId : SingleValueObject<string>, ISagaId
{
    public SignInSagaId(string value) : base(value)
    {
    }
}
