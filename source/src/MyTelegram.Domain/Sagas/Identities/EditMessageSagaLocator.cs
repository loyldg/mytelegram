namespace MyTelegram.Domain.Sagas.Identities;

public class EditMessageSagaLocator : DefaultSagaLocator<EditMessageSaga, EditMessageSagaId>
{
    protected override EditMessageSagaId CreateSagaId(string requestId)
    {
        return new EditMessageSagaId(requestId);
    }
}