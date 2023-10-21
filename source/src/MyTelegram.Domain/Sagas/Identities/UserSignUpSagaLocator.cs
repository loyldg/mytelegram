namespace MyTelegram.Domain.Sagas.Identities;

public class UserSignUpSagaLocator : DefaultSagaLocator<UserSignUpSaga, UserSignUpSagaId>
{
    protected override UserSignUpSagaId CreateSagaId(string requestId)
    {
        return new UserSignUpSagaId(requestId);
    }
}