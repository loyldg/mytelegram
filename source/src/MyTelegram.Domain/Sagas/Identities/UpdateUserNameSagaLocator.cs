namespace MyTelegram.Domain.Sagas.Identities;

public class UpdateUserNameSagaLocator : DefaultSagaLocator<UpdateUserNameSaga, UpdateUserNameSagaId>
{
    protected override UpdateUserNameSagaId CreateSagaId(string requestId)
    {
        return new UpdateUserNameSagaId(requestId);
    }
}