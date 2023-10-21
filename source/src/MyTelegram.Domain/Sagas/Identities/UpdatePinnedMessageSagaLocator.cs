namespace MyTelegram.Domain.Sagas.Identities;

public class UpdatePinnedMessageSagaLocator : DefaultSagaLocator<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>
{
    protected override UpdatePinnedMessageSagaId CreateSagaId(string requestId)
    {
        return new UpdatePinnedMessageSagaId(requestId);
    }
}