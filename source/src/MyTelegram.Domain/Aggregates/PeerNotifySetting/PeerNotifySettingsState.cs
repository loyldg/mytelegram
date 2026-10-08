namespace MyTelegram.Domain.Aggregates.PeerNotifySetting;

public class PeerNotifySettingsState :
    AggregateState<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsState>,
    IApply<PeerNotifySettingsUpdatedEvent>,
    IApply<PeerNotifySettingsUpdatedEvent2>
{
    public MyTelegram.PeerNotifySettings PeerNotifySettings { get; private set; } = default!;

    public void Apply(PeerNotifySettingsUpdatedEvent aggregateEvent)
    {
        PeerNotifySettings = aggregateEvent.PeerNotifySettings;
    }

    public void LoadSnapshot(PeerNotifySettingsSnapshot snapshot)
    {
        PeerNotifySettings = snapshot.PeerNotifySettings;
    }

    public void Apply(PeerNotifySettingsUpdatedEvent2 aggregateEvent)
    {
        
    }
}
