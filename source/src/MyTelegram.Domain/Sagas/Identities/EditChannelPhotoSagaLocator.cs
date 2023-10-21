namespace MyTelegram.Domain.Sagas.Identities;

public class EditChannelPhotoSagaLocator : DefaultSagaLocator<EditChannelPhotoSaga, EditChannelPhotoSagaId>
{
    protected override EditChannelPhotoSagaId CreateSagaId(string requestId)
    {
        return new EditChannelPhotoSagaId(requestId);
    }
}