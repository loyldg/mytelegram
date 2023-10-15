namespace MyTelegram.Domain.Aggregates.Pts;

//[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ChannelPtsId>))]
//public class ChannelPtsId:MyIdentity<ChannelPtsId>
//{
//    public ChannelPtsId(string value) : base(value)
//    {
//    }

//    public static ChannelPtsId Create(long userId)
//    {
//        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"channelpts_{userId}");
//    }
//}

//public class ChannelPtsSnapshot : ISnapshot
//{
//    public ChannelPtsSnapshot(long userId, long globalSeqNo)
//    {
//        UserId = userId;
//        GlobalSeqNo = globalSeqNo;
//    }

//    public long UserId { get;  }
//    public long GlobalSeqNo { get; }
//}

//public class ChannelPtsState : AggregateState<ChannelPtsAggregate,ChannelPtsId,ChannelPtsState>,
//    IApply<ChannelPtsForUserUpdatedEvent>
//{
//    public long UserId { get; private set; }
//    public long GlobalSeqNo { get; private set; }
//    public void Apply(ChannelPtsForUserUpdatedEvent aggregateEvent)
//    {
//        UserId=aggregateEvent.UserId;
//        GlobalSeqNo=aggregateEvent.GlobalSeqNo;
//    }

//    public void LoadSnapshot(ChannelPtsSnapshot snapshot)
//    {
//        UserId=snapshot.UserId;
//        GlobalSeqNo=snapshot.GlobalSeqNo;
//    }
//}

//public class ChannelPtsAggregate : MyInMemorySnapshotAggregateRoot<ChannelPtsAggregate, ChannelPtsId, ChannelPtsSnapshot>,
//    INotSaveAggregateEvents
//{
//    private readonly ChannelPtsState _state = new();
//    public ChannelPtsAggregate(ChannelPtsId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
//    {
//        Register(_state);
//    }

//    public void UpdateChannelPtsForUser(long userId, long channelId, int pts, long globalSeqNo)
//    {
//        Emit(new ChannelPtsForUserUpdatedEvent(userId, channelId, pts, globalSeqNo));
//    }

//    protected override Task<ChannelPtsSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
//    {
//        return Task.FromResult(new ChannelPtsSnapshot(_state.UserId, _state.GlobalSeqNo));
//    }

//    protected override Task LoadSnapshotAsync(ChannelPtsSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
//    {
//        _state.LoadSnapshot(snapshot);
//        return Task.CompletedTask;
//    }
//}

public class PtsAggregate : MyInMemorySnapshotAggregateRoot<PtsAggregate, PtsId, PtsSnapshot>, INotSaveAggregateEvents
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
            _state.GlobalSeqNo, _state.PermAuthKeyId));
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
        int newPts, long globalSeqNo, int changedUnreadCount)
    {
        if (!IsNew)
        {
            if (_state.Pts < newPts)
            {
                Emit(new PtsUpdatedEvent(peerId, permAuthKeyId, newPts, DateTime.UtcNow.ToTimestamp(), globalSeqNo, changedUnreadCount));
            }
        }
        else
        {
            Emit(new PtsUpdatedEvent(peerId, permAuthKeyId, newPts, DateTime.UtcNow.ToTimestamp(), globalSeqNo, changedUnreadCount));
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

    public void UpdatePtsForAuthKeyId(long peerId,
        long permAuthKeyId,
        int pts,
        int changedUnreadCount,
        long globalSeqNo)
    {
        Emit(new PtsForAuthKeyIdUpdatedEvent(peerId, permAuthKeyId, pts, changedUnreadCount, globalSeqNo));
    }

    //public void UpdateChannelPtsForUser(long userId, long channelId, int pts, long globalSeqNo)
    //{
    //    Emit(new ChannelPtsForUserUpdatedEvent(userId, channelId,pts,globalSeqNo));
    //}
}