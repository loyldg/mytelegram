namespace MyTelegram.Domain.Sagas.Identities;

public class DeleteMessageSagaLocator : DefaultSagaLocator<DeleteMessageSaga, DeleteMessageSagaId>
{
    protected override DeleteMessageSagaId CreateSagaId(string requestId)
    {
        return new DeleteMessageSagaId(requestId);
    }
}