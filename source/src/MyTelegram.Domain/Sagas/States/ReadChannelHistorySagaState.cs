namespace MyTelegram.Domain.Sagas.States;

public class ReadChannelHistorySagaState : AggregateState<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaState>,
    IApply<ReadChannelHistoryStartedEvent>
{
    public long ChannelId { get; private set; }
    public Guid CorrelationId { get; private set; }
    public long ReaderUid { get; private set; }

    public long ReqMsgId { get; private set; }
    //public bool NeedWa

    public void Apply(ReadChannelHistoryStartedEvent aggregateEvent)
    {
        ReqMsgId = aggregateEvent.ReqMsgId;
        ReaderUid = aggregateEvent.ReaderUid;
        ChannelId = aggregateEvent.ChannelId;
        CorrelationId = aggregateEvent.CorrelationId;
    }

    public void LoadSnapshot(ReadChannelHistorySagaSnapshot snapshot)
    {
        ReqMsgId = snapshot.ReqMsgId;
        ReaderUid = snapshot.ReaderUid;
        ChannelId = snapshot.ChannelId;
        CorrelationId = snapshot.CorrelationId;
    }
}
