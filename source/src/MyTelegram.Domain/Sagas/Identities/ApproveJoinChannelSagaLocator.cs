namespace MyTelegram.Domain.Sagas.Identities;

public class ApproveJoinChannelSagaLocator : DefaultSagaLocator<ApproveJoinChannelSaga,ApproveJoinChannelSagaId>
{
    protected override ApproveJoinChannelSagaId CreateSagaId(string sagaId)
    {
        return new ApproveJoinChannelSagaId(sagaId);
    }
}