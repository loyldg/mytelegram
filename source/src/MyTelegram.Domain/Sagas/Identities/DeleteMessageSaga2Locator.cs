namespace MyTelegram.Domain.Sagas.Identities;

public class DeleteMessageSaga2Locator : DefaultSagaLocator<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    protected override DeleteMessageSaga2Id CreateSagaId(string requestId)
    {
        return new DeleteMessageSaga2Id(requestId);
    }
}