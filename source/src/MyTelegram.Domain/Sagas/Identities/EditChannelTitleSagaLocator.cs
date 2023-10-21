namespace MyTelegram.Domain.Sagas.Identities;

public class EditChannelTitleSagaLocator : DefaultSagaLocator<EditChannelTitleSaga, EditChannelTitleSagaId>
{
    protected override EditChannelTitleSagaId CreateSagaId(string requestId)
    {
        return new EditChannelTitleSagaId(requestId);
    }
}