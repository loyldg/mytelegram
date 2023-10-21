namespace MyTelegram.Domain.Sagas.Identities;

public class SignInSagaLocator : DefaultSagaLocator<SignInSaga, SignInSagaId>
{
    protected override SignInSagaId CreateSagaId(string requestId)
    {
        return new SignInSagaId(requestId);
    }
}