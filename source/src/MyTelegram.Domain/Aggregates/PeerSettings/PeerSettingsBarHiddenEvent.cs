namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsBarHiddenEvent : AggregateEvent<PeerSettingsAggregate, PeerSettingsId>
{
    public long OwnerPeerId { get; }
    public long PeerId { get; }

    public PeerSettingsBarHiddenEvent(long ownerPeerId, long peerId)
    {
        OwnerPeerId = ownerPeerId;
        PeerId = peerId;
    }
}