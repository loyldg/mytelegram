namespace MyTelegram.Domain.Aggregates.RpcResult;

public class RpcResultAggregate : MyInMemorySnapshotAggregateRoot<RpcResultAggregate, RpcResultId, RpcResultSnapshot>, INotSaveAggregateEvents
{
    private readonly RpcResultState _state = new();

    public RpcResultAggregate(RpcResultId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void DeleteRpcResult()
    {
        Emit(new RpcResultDeletedEvent());
    }

    public void CreateRpcResult(RequestInfo requestInfo,
        byte[] rpcData)
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var date = DateTime.UtcNow.ToTimestamp();

        Emit(new RpcResultCreatedEvent(requestInfo, rpcData, date));
    }

    protected override Task<RpcResultSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new RpcResultSnapshot());
    }

    protected override Task LoadSnapshotAsync(RpcResultSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
