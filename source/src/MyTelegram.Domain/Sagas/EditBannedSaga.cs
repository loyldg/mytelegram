namespace MyTelegram.Domain.Sagas;

public class EditBannedSaga(
    EditBannedSagaId id,
    IEventStore eventStore)
    : MyInMemoryAggregateSaga<EditBannedSaga, EditBannedSagaId, EditBannedSagaLocator>(id, eventStore),
        ISagaIsStartedBy<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        // view message rights is banned,should remove from bots
        if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
        {

            var updateParticipantCountCommand = new UpdateParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId), -1);
            Publish(updateParticipantCountCommand);
        }

        if (domainEvent.AggregateEvent.Banned && domainEvent.AggregateEvent.IsAdmin)
        {
            var command = new RemoveChannelAdminCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
                domainEvent.AggregateEvent.MemberUserId);
            Publish(command);
        }

        return CompleteAsync(cancellationToken);
    }
}
