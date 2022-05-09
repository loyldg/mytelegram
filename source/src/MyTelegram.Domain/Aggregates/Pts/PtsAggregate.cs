namespace MyTelegram.Domain.Aggregates.Pts;

public class PtsAggregate : MyInMemorySnapshotAggregateRoot<PtsAggregate, PtsId, PtsSnapshot>
{
    private readonly PtsState _state = new();

    public PtsAggregate(PtsId id) : base(id,
        SnapshotEveryFewVersionsStrategy.Default
    )
    {
        Register(_state);
    }

    protected override Task<PtsSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new PtsSnapshot(_state.PeerId,
            _state.Pts,
            _state.Qts,
            _state.UnreadCount,
            _state.Date,
            _state.GlobalSeqNo));
    }

    protected override Task LoadSnapshotAsync(PtsSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }

    public void PtsAcked(long peerId,
        long permAuthKeyId,
        long msgId,
        int pts,
        long globalSeqNo,
        Peer toPeer)
    {
        Emit(new PtsAckedEvent(peerId,
            permAuthKeyId,
            msgId,
            pts,
            globalSeqNo,
            toPeer));
    }

    public void UpdateGlobalSeqNo(long peerId,
        long permAuthKeyId,
        long globalSeqNo)
    {
        Emit(new PtsGlobalSeqNoUpdatedEvent(peerId, permAuthKeyId, globalSeqNo));
    }

    public void UpdatePts(long peerId,
        long permAuthKeyId,
        int newPts)
    {
        if (!IsNew)
        {
            if (_state.Pts < newPts)
            {
                Emit(new PtsUpdatedEvent(peerId, permAuthKeyId, newPts));
            }
        }
        else
        {
            Emit(new PtsUpdatedEvent(peerId, permAuthKeyId, newPts));
        }
    }

    public void UpdateQts(long peerId,
        int newQts)
    {
        if (!IsNew)
        {
            if (_state.Qts < newQts)
            {
                Emit(new QtsUpdatedEvent(peerId, newQts));
            }
        }
        else
        {
            Emit(new QtsUpdatedEvent(peerId, newQts));
        }
    }
}
