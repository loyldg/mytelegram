namespace MyTelegram.Domain.Sagas.Identities;

public class ForwardMessageSagaLocator : DefaultSagaLocator<ForwardMessageSaga, ForwardMessageSagaId>
{
    protected override ForwardMessageSagaId CreateSagaId(string requestId)
    {
        return new ForwardMessageSagaId(requestId);
    }
}