namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsAggregate : SnapshotAggregateRoot<PeerSettingsAggregate, PeerSettingsId, PeerSettingsSnapshot>
{
    private readonly PeerSettingsState _state = new();
    public PeerSettingsAggregate(PeerSettingsId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void HidePeerSettingsBar(RequestInfo requestInfo, long targetPeerId)
    {
        Emit(new PeerSettingsBarHiddenEvent(requestInfo.UserId, targetPeerId));
    }

    protected override Task<PeerSettingsSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new PeerSettingsSnapshot(_state.PeerSettings, _state.HidePeerSettingsBar, _state.OwnerPeerId, _state.PeerId));
    }

    protected override Task LoadSnapshotAsync(PeerSettingsSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}