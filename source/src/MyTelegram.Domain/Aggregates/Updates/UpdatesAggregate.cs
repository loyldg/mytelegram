using MyTelegram.Domain.Events.Updates;

namespace MyTelegram.Domain.Aggregates.Updates;

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
        IList<IUpdate>? updates,
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