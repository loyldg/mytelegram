namespace MyTelegram.ReadModel.Impl;

public class PeerNotifySettingsReadModel : IPeerNotifySettingsReadModel,
    IAmReadModelFor<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        //ShowPreviews = domainEvent.AggregateEvent.ShowPreviews;
        //Silent = domainEvent.AggregateEvent.Silent;
        //MuteUntil = domainEvent.AggregateEvent.MuteUntil;
        //Sound = domainEvent.AggregateEvent.Sound;
        OwnerPeerId = domainEvent.AggregateEvent.OwnerPeerId;
        PeerType = domainEvent.AggregateEvent.PeerType;
        PeerId = domainEvent.AggregateEvent.PeerId;

        NotifySettings = domainEvent.AggregateEvent.PeerNotifySettings;
        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual PeerNotifySettings NotifySettings { get; protected set; } = null!;
    public virtual long OwnerPeerId { get; private set; }
    public virtual long PeerId { get; private set; }

    public virtual PeerType PeerType { get; private set; }
    //public bool ShowPreviews { get; private set; } = true;
    //public bool Silent { get; private set; }
    //public int MuteUntil { get; private set; }// = int.MaxValue;
    //public string Sound { get; private set; } = "default";
}
