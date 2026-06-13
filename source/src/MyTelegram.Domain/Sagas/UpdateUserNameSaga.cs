namespace MyTelegram.Domain.Sagas;

public class UpdateUserNameSaga(UpdateUserNameSagaId id, IEventStore eventStore)
    : MyInMemoryAggregateSaga<UpdateUserNameSaga, UpdateUserNameSagaId, UpdateUserNameSagaLocator>(id, eventStore),
        ISagaIsStartedBy<UserNameAggregate, UserNameId, UserNameChangedEvent>,
        ISagaHandles<UserAggregate, UserId, UserNameUpdatedEvent>,
        ISagaHandles<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
        IApply<UpdateUserNameStartedSagaEvent>
{
    public void Apply(UpdateUserNameStartedSagaEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.OldUserName))
        {
            var command = new DeleteUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.OldUserName));
            Publish(command);
        }

       return CompleteAsync();
    }

    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.OldUserName))
        {
            var command = new DeleteUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.OldUserName.ToLower()));
            Publish(command);
        }

        return CompleteAsync();
    }

    public Task HandleAsync(IDomainEvent<UserNameAggregate, UserNameId, UserNameChangedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        switch (domainEvent.AggregateEvent.Peer.PeerType)
        {
            case PeerType.User:
                {
                    var command = new UpdateUserNameCommand2(UserId.Create(domainEvent.AggregateEvent.Peer.PeerId),
                        domainEvent.AggregateEvent.RequestInfo,
                        domainEvent.AggregateEvent.UserName ?? string.Empty
                    );
                    Publish(command);
                }
                break;

            case PeerType.Channel:
                {
                    var command = new UpdateChannelUserNameCommand(ChannelId.Create(domainEvent.AggregateEvent.Peer.PeerId),
                        domainEvent.AggregateEvent.RequestInfo,
                        //domainEvent.AggregateEvent.Peer.PeerId,
                        domainEvent.AggregateEvent.UserName ?? string.Empty
                    );
                    Publish(command);
                }
                break;
        }

        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.OldUserName))
        {
            var command = new DeleteUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.OldUserName.ToLower()));
            Publish(command);
        }

        return Task.CompletedTask;
    }
}
