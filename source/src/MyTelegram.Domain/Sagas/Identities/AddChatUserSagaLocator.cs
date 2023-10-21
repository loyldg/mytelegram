namespace MyTelegram.Domain.Sagas.Identities;

public class AddChatUserSagaLocator : DefaultSagaLocator<AddChatUserSaga, AddChatUserSagaId>
{
    protected override AddChatUserSagaId CreateSagaId(string sagaId)
    {
        return new AddChatUserSagaId(sagaId);
    }
}