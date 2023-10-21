namespace MyTelegram.Domain.Sagas.Identities;

public class DeleteChatUserSagaLocator : DefaultSagaLocator<DeleteChatUserSaga, DeleteChatUserSagaId>
{
    protected override DeleteChatUserSagaId CreateSagaId(string requestId)
    {
        return new DeleteChatUserSagaId(requestId);
    }
}