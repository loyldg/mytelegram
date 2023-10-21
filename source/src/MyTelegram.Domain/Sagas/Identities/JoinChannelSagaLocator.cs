namespace MyTelegram.Domain.Sagas.Identities;

public class JoinChannelSagaLocator : DefaultSagaLocator<JoinChannelSaga, JoinChannelSagaId>
{
    protected override JoinChannelSagaId CreateSagaId(string requestId)
    {
        return new JoinChannelSagaId(requestId);
    }
}