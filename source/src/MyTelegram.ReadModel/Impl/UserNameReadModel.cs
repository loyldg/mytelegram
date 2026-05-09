namespace MyTelegram.ReadModel.Impl;

public class UserNameReadModel : ReadModelBase, IUserNameReadModel,
    IAmReadModelFor<UserNameAggregate, UserNameId, UserNameDeletedEvent>,
    IAmReadModelFor<UserNameAggregate, UserNameId, UserNameCreatedEvent>,
    IAmReadModelFor<UserNameAggregate, UserNameId, UserNameChangedEvent>

{
    public virtual string Id { get; private set; } = null!;
    public virtual long PeerId { get; private set; }
    public virtual PeerType PeerType { get; private set; }
    public virtual string UserName { get; private set; } = null!;
    public int Date { get; private set; }
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserNameAggregate, UserNameId, UserNameDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }
    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserNameAggregate, UserNameId, UserNameCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        UserName = domainEvent.AggregateEvent.UserName;
        PeerId = domainEvent.AggregateEvent.Peer.PeerId;
        PeerType = domainEvent.AggregateEvent.Peer.PeerType;
        Date = domainEvent.AggregateEvent.Date;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UserNameAggregate, UserNameId, UserNameChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(domainEvent.AggregateEvent.UserName))
        {
            context.MarkForDeletion();
        }
        else
        {
            Id = domainEvent.AggregateIdentity.Value;
            UserName = domainEvent.AggregateEvent.UserName;
            PeerId = domainEvent.AggregateEvent.Peer.PeerId;
            PeerType = domainEvent.AggregateEvent.Peer.PeerType;
            Date = domainEvent.AggregateEvent.Date;
        }

        return Task.CompletedTask;
    }
}
