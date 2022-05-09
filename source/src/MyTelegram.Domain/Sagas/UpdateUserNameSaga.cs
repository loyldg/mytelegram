namespace MyTelegram.Domain.Sagas;

public class UpdateUserNameSaga : AggregateSaga<UpdateUserNameSaga, UpdateUserNameSagaId, UpdateUserNameSagaLocator>,
    ISagaIsStartedBy<UserNameAggregate, UserNameId, SetUserNameSuccessEvent>,
    ISagaHandles<UserAggregate, UserId, UserNameUpdatedEvent>,
    ISagaHandles<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
    IApply<UpdateUserNameStartedEvent>
{
    public UpdateUserNameSaga(UpdateUserNameSagaId id) : base(id)
    {
    }

    public void Apply(UpdateUserNameStartedEvent aggregateEvent)
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

        Complete();

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        //Console.WriteLine("old user name deleted.");
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.OldUserName))
        {
            var command = new DeleteUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.OldUserName));
            Publish(command);
        }

        Complete();

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<UserNameAggregate, UserNameId, SetUserNameSuccessEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new UpdateUserNameStartedEvent());

        switch (domainEvent.AggregateEvent.PeerType)
        {
            case PeerType.User:
            {
                var command = new UpdateUserNameCommand(UserId.Create(domainEvent.AggregateEvent.PeerId),
                    domainEvent.AggregateEvent.ReqMsgId,
                    domainEvent.AggregateEvent.UserName,
                    domainEvent.AggregateEvent.CorrelationId
                );
                Publish(command);
            }
                break;
            case PeerType.Channel:
            {
                var command = new UpdateChannelUserNameCommand(ChannelId.Create(domainEvent.AggregateEvent.PeerId),
                    domainEvent.AggregateEvent.ReqMsgId,
                    domainEvent.AggregateEvent.PeerId,
                    domainEvent.AggregateEvent.UserName,
                    domainEvent.AggregateEvent.CorrelationId
                );
                Publish(command);
            }
                break;
        }

        return Task.CompletedTask;
    }
}
