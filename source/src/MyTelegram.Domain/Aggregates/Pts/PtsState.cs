namespace MyTelegram.Domain.Aggregates.Pts;

public class PtsState : AggregateState<PtsAggregate, PtsId, PtsState>,
    IApply<PtsUpdatedEvent>,
    IApply<QtsUpdatedEvent>,
    IApply<PtsAckedEvent>,
    IApply<PtsGlobalSeqNoUpdatedEvent>,
    IApply<PtsForAuthKeyIdUpdatedEvent>//,
    //IApply<ChannelPtsForUserUpdatedEvent>
{
    public int Date { get; private set; }
    public long GlobalSeqNo { get; private set; }
    public long PeerId { get; private set; }
    public int Pts { get; private set; }
    public int Qts { get; private set; }
    public int UnreadCount { get; private set; }
    public long PermAuthKeyId { get; private set; }

    public void Apply(PtsAckedEvent aggregateEvent)
    {
        if (aggregateEvent.Pts != 0)
        {
            Pts = aggregateEvent.Pts;
        }

        if (aggregateEvent.GlobalSeqNo != 0)
        {
            GlobalSeqNo = aggregateEvent.GlobalSeqNo;
        }
    }

    public void Apply(PtsGlobalSeqNoUpdatedEvent aggregateEvent)
    {
        GlobalSeqNo = aggregateEvent.GlobalSeqNo;
    }

    public void Apply(PtsUpdatedEvent aggregateEvent)
    {
        PeerId = aggregateEvent.PeerId;
        Pts = aggregateEvent.NewPts;
    }

    public void Apply(QtsUpdatedEvent aggregateEvent)
    {
        PeerId = aggregateEvent.PeerId;
        Qts = aggregateEvent.NewQts;
    }

    public void Apply(PtsCreatedEvent aggregateEvent)
    {
        PeerId = aggregateEvent.PeerId;
        Pts = aggregateEvent.Pts;
        Qts = aggregateEvent.Qts;
        UnreadCount = aggregateEvent.UnreadCount;
        Date = aggregateEvent.Date;
    }

    public void LoadSnapshot(PtsSnapshot snapshot)
    {
        PeerId = snapshot.PeerId;
        Pts = snapshot.Pts;
        Qts = snapshot.Qts;
        UnreadCount = snapshot.UnreadCount;
        Date = snapshot.Date;
        GlobalSeqNo = snapshot.GlobalSeqNo;
        PermAuthKeyId= snapshot.PermAuthKeyId;
    }

    public void Apply(PtsForAuthKeyIdUpdatedEvent aggregateEvent)
    {
        PeerId = aggregateEvent.PeerId;
        Pts = aggregateEvent.Pts;
        GlobalSeqNo= aggregateEvent.GlobalSeqNo;
        PermAuthKeyId= aggregateEvent.PermAuthKeyId;
    }

    //public void Apply(ChannelPtsForUserUpdatedEvent aggregateEvent)
    //{
    //    PeerId = aggregateEvent.UserId;
    //    Pts= aggregateEvent.Pts;
    //    GlobalSeqNo=aggregateEvent.GlobalSeqNo;
    //}
}
