namespace MyTelegram.ReadModel.Impl;

public class UserNameReadModel : IUserNameReadModel,
    IAmReadModelFor<UserNameAggregate, UserNameId, SetUserNameSuccessEvent>,
    IAmReadModelFor<UserNameAggregate, UserNameId, UserNameDeletedEvent>

{
    public virtual long? Version { get; set; }
    //public void Apply(IReadModelContext context,
    //    IDomainEvent<UserNameAggregate, UserNameId, SetUserNameSuccessEvent> domainEvent)
    //{
    //    Id = domainEvent.AggregateIdentity.Value;
    //    UserName = domainEvent.AggregateEvent.UserName;
    //    PeerType = domainEvent.AggregateEvent.PeerType;
    //    PeerId = domainEvent.AggregateEvent.PeerId;
    //}

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserNameAggregate, UserNameId, SetUserNameSuccessEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        UserName = domainEvent.AggregateEvent.UserName;
        PeerType = domainEvent.AggregateEvent.PeerType;
        PeerId = domainEvent.AggregateEvent.PeerId;

        return Task.CompletedTask;
    }

    //public void Apply(IReadModelContext context,
    //    IDomainEvent<UserNameAggregate, UserNameId, UserNameDeletedEvent> domainEvent)
    //{
    //    context.MarkForDeletion();
    //}

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<UserNameAggregate, UserNameId, UserNameDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        context.MarkForDeletion();
        return Task.CompletedTask;
    }

    public virtual string Id { get; private set; } = null!;
    public virtual string UserName { get; private set; } = null!;
    public virtual PeerType PeerType { get; private set; }
    public virtual long PeerId { get; private set; }
}
