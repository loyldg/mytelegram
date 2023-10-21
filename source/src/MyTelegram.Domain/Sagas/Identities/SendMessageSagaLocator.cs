namespace MyTelegram.Domain.Sagas.Identities;

public class SendMessageSagaLocator : DefaultSagaLocator<SendMessageSaga, SendMessageSagaId>
{
    protected override SendMessageSagaId CreateSagaId(string requestId)
    {
        return new SendMessageSagaId(requestId);
    }
}