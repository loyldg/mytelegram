namespace MyTelegram.Domain.Sagas.Identities;

public class ClearHistorySagaLocator : DefaultSagaLocator<ClearHistorySaga, ClearHistorySagaId>
{
    protected override ClearHistorySagaId CreateSagaId(string sagaId)
    {
        return new ClearHistorySagaId(sagaId);
    }
}