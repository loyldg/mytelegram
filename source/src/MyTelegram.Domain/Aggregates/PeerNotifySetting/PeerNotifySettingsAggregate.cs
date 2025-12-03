namespace MyTelegram.Domain.Aggregates.PeerNotifySetting;

public class PeerNotifySettingsAggregate : SnapshotAggregateRoot<PeerNotifySettingsAggregate, PeerNotifySettingsId,
    PeerNotifySettingsSnapshot>
{
    private readonly PeerNotifySettingsState _state = new();

    public PeerNotifySettingsAggregate(PeerNotifySettingsId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdatePeerNotifySettings(RequestInfo requestInfo,
        long ownerPeerId,
        PeerType peerType,
        long peerId,
        bool? showPreviews,
        bool? silent,
        int? muteUntil,
        string? sound)
    {
        var peerNotifySettings = new PeerNotifySettings(showPreviews, silent, muteUntil, sound);
        Emit(new PeerNotifySettingsUpdatedEvent(requestInfo,
            ownerPeerId,
            peerType,
            peerId,
            peerNotifySettings));
    }

    protected override Task<PeerNotifySettingsSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new PeerNotifySettingsSnapshot(_state.PeerNotifySettings));
    }

    protected override Task LoadSnapshotAsync(PeerNotifySettingsSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}