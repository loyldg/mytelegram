namespace MyTelegram.Domain.Aggregates.PeerNotifySettings;

public class PeerNotifySettingsAggregate : SnapshotAggregateRoot<PeerNotifySettingsAggregate, PeerNotifySettingsId,
    PeerNotifySettingsSnapshot>
{
    private readonly PeerNotifySettingsState _state = new();

    public PeerNotifySettingsAggregate(PeerNotifySettingsId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdatePeerNotifySettings(long reqMsgId,
        long ownerPeerId,
        PeerType peerType,
        long peerId,
        bool? showPreviews,
        bool? silent,
        int? muteUntil,
        string? sound)
    {
        Emit(new PeerNotifySettingsUpdatedEvent(reqMsgId,
            ownerPeerId,
            peerType,
            peerId,
            new ValueObjects.PeerNotifySettings(showPreviews, silent, muteUntil, sound)));
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
