namespace MyTelegram.Domain.Sagas;

public class JoinChannelSaga : AggregateSaga<JoinChannelSaga, JoinChannelSagaId, JoinChannelSagaLocator>,
    ISagaIsStartedBy<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>
{
    public JoinChannelSaga(JoinChannelSagaId id) : base(id)
    {
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);
        Complete();
        return Task.CompletedTask;
    }
}
