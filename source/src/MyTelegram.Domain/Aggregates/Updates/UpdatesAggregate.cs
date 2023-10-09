using MyTelegram.Domain.Events.Updates;

namespace MyTelegram.Domain.Aggregates.Updates;

public class UpdatesSnapshot : ISnapshot { }

public class UpdatesAggregate : MyInMemorySnapshotAggregateRoot<UpdatesAggregate, UpdatesId, UpdatesSnapshot>, INotSaveAggregateEvents
{
    private readonly UpdatesState _state = new();
    public UpdatesAggregate(UpdatesId id) : base(id,SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void Create(long ownerPeerId, long? excludeAuthKeyId, long? excludeUserId,
        long? onlySendToUserId,
        long? onlySendToThisAuthKeyId,
        UpdatesType updatesType, int pts, int? messageId, int date, long globalSeqNo,
        byte[] updates,
        List<long>? users, List<long>? chats)
    {
        Emit(new UpdatesCreatedEvent(ownerPeerId,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            updatesType,
            pts,
            messageId,
            date,
            globalSeqNo,
            updates,
            users,
            chats
        ));
    }

    protected override Task<UpdatesSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new UpdatesSnapshot());
    }

    protected override Task LoadSnapshotAsync(UpdatesSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }
}

public class UpdatesState : AggregateState<UpdatesAggregate, UpdatesId, UpdatesState>,
    IApply<UpdatesCreatedEvent>
{
    //public long OwnerPeerId { get; private set; }
    //public long? ExcludeAuthKeyId { get; private set; }
    //public long? ExcludeUserId { get; private set; }
    //public long? OnlySendToThisAuthKeyId { get; private set; }
    //public PtsType PtsType { get; private set; }
    //public int Pts { get; private set; }
    //public int? MessageId { get; private set; }
    //public int Date { get; private set; }
    //public long SeqNo { get; private set; }
    //public byte[] Updates { get; private set; }
    //public List<long>? Users { get; private set; }
    //public List<long>? Chats { get; private set; }
    public void Apply(UpdatesCreatedEvent aggregateEvent)
    {

    }
}