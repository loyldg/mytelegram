namespace MyTelegram.Domain.Sagas.Identities;

public class ReadHistorySagaLocator : DefaultSagaLocator<ReadHistorySaga, ReadHistorySagaId>
{
    protected override ReadHistorySagaId CreateSagaId(string requestId)
    {
        return new ReadHistorySagaId(requestId);
    }
}