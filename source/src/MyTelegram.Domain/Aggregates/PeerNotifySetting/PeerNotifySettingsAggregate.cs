namespace MyTelegram.Domain.Aggregates.PeerNotifySetting;

[EnableAutoGeneration]
public class PeerNotifySettingsAggregate : SnapshotAggregateRoot<PeerNotifySettingsAggregate, PeerNotifySettingsId,
    PeerNotifySettingsSnapshot>
{
    private readonly PeerNotifySettingsState _state = new();

    public PeerNotifySettingsAggregate(PeerNotifySettingsId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdatePeerNotifySettings2(RequestInfo requestInfo,
        long ownerUserId,
        PeerNotifyType peerNotifyType,
        long toPeerId,
        IPeerNotifySettings peerNotifySettings
        )
    {
        Emit(new PeerNotifySettingsUpdatedEvent2(requestInfo, ownerUserId, peerNotifyType, toPeerId, peerNotifySettings));
    }

    public void UpdatePeerNotifySettings(RequestInfo requestInfo,
        long ownerPeerId,
        PeerNotifyType peerNotifyType,
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
            peerNotifyType,
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