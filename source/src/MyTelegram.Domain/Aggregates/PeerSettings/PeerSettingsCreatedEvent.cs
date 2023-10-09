namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsCreatedEvent : AggregateEvent<PeerSettingsAggregate, PeerSettingsId>
{
    public ValueObjects.PeerSettings PeerSettings { get; }

    public PeerSettingsCreatedEvent(ValueObjects.PeerSettings peerSettings)
    {
        PeerSettings = peerSettings;
    }
}