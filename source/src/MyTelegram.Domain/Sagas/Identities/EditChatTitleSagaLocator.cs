namespace MyTelegram.Domain.Sagas.Identities;

public class EditChatTitleSagaLocator : DefaultSagaLocator<EditChatTitleSaga, EditChatTitleSagaId>
{
    protected override EditChatTitleSagaId CreateSagaId(string requestId)
    {
        return new EditChatTitleSagaId(requestId);
    }
}