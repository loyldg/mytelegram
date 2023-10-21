namespace MyTelegram.Domain.Sagas.Identities;

public class ReadChannelHistorySagaLocator : DefaultSagaLocator<ReadChannelHistorySaga, ReadChannelHistorySagaId>
{
    protected override ReadChannelHistorySagaId CreateSagaId(string requestId)
    {
        return new ReadChannelHistorySagaId(requestId);
    }
}