namespace MyTelegram.ReadModel.Impl;

public class PeerSettingsReadModel : IPeerSettingsReadModel,
    IAmReadModelFor<PeerSettingsAggregate, PeerSettingsId, PeerSettingsBarHiddenEvent>
{
    public string Id { get; private set; }
    public long OwnerPeerId { get; private set; }
    public long PeerId { get; private set; }
    public PeerSettings? PeerSettings { get; private set; }
    public bool HiddenPeerSettingsBar { get; private set; }

    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PeerSettingsAggregate, PeerSettingsId, PeerSettingsBarHiddenEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        OwnerPeerId = domainEvent.AggregateEvent.OwnerPeerId;
        PeerId = domainEvent.AggregateEvent.PeerId;
        HiddenPeerSettingsBar = true;

        return Task.CompletedTask;
    }
}