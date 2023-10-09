namespace MyTelegram.Domain.Sagas.States;

public class ReadChannelHistorySagaState : AggregateState<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaState>,
    IApply<ReadChannelHistoryStartedEvent>
{
    public RequestInfo RequestInfo { get; set; }
    public long ChannelId { get; private set; }
    public long ReaderUserId { get; private set; }

    //public bool NeedWa
    public int? TopMsgId { get; private set; }

    public void Apply(ReadChannelHistoryStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ReaderUserId = aggregateEvent.ReaderUserId;
        ChannelId = aggregateEvent.ChannelId;
        TopMsgId = aggregateEvent.TopMsgId;
    }

    //public void LoadSnapshot(ReadChannelHistorySagaSnapshot snapshot)
    //{
    //    ReqMsgId = snapshot.ReqMsgId;
    //    ReaderUid = snapshot.ReaderUid;
    //    ChannelId = snapshot.ChannelId;
    //    CorrelationId = snapshot.CorrelationId;
    //}
}