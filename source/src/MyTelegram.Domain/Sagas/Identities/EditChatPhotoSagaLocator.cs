namespace MyTelegram.Domain.Sagas.Identities;

public class EditChatPhotoSagaLocator : DefaultSagaLocator<EditChatPhotoSaga, EditChatPhotoSagaId>
{
    protected override EditChatPhotoSagaId CreateSagaId(string requestId)
    {
        return new EditChatPhotoSagaId(requestId);
    }
}