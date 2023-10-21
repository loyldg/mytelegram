namespace MyTelegram.Domain.Sagas.Identities;

public class DeleteParticipantHistorySagaLocator : DefaultSagaLocator<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId>
{
    protected override DeleteParticipantHistorySagaId CreateSagaId(string requestId)
    {
        return new DeleteParticipantHistorySagaId(requestId);
    }
}