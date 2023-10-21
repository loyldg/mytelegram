namespace MyTelegram.Domain.Sagas.Identities;

public class CreateChannelSagaLocator : DefaultSagaLocator<CreateChannelSaga, CreateChannelSagaId>
{
    protected override CreateChannelSagaId CreateSagaId(string sagaId)
    {
        return new CreateChannelSagaId(sagaId);
    }
}