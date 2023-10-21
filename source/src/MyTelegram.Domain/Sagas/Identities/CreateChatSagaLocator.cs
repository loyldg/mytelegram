namespace MyTelegram.Domain.Sagas.Identities;

public class CreateChatSagaLocator : DefaultSagaLocator<CreateChatSaga, CreateChatSagaId>
{
    protected override CreateChatSagaId CreateSagaId(string requestId)
    {
        return new CreateChatSagaId(requestId);
    }
}