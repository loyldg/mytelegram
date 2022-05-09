namespace MyTelegram.Domain.Aggregates.PushUpdates;

public class PushUpdatesState : AggregateState<PushUpdatesAggregate, PushUpdatesId, PushUpdatesState>,
    IApply<PushUpdatesCreatedEvent>,
    IApply<EncryptedPushUpdatesCreatedEvent>
{
    public byte[] Data { get; private set; } = default!;
    public long ExcludeAuthKeyId { get; private set; }
    public long ExcludeUid { get; private set; }
    public long OnlySendToThisAuthKeyId { get; private set; }
    public long PeerId { get; private set; }
    public int Pts { get; private set; }
    public PtsType PtsType { get; private set; }
    public long SeqNo { get; private set; }

    public void Apply(EncryptedPushUpdatesCreatedEvent aggregateEvent)
    {
    }

    public void Apply(PushUpdatesCreatedEvent aggregateEvent)
    {
        PeerId = aggregateEvent.ToPeer.PeerId;
        ExcludeAuthKeyId = aggregateEvent.ExcludeAuthKeyId;
        ExcludeUid = aggregateEvent.ExcludeUid;
        OnlySendToThisAuthKeyId = aggregateEvent.OnlySendToThisAuthKeyId;
        Data = aggregateEvent.Data;
        Pts = aggregateEvent.Pts;
        PtsType = aggregateEvent.PtsType;
        SeqNo = aggregateEvent.SeqNo;
    }
}
