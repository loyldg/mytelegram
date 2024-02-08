using MyTelegram.Schema.Extensions;

namespace MyTelegram.Domain.Sagas;

public class ApproveJoinChannelSaga : MyInMemoryAggregateSaga<ApproveJoinChannelSaga, ApproveJoinChannelSagaId, ApproveJoinChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent>
{
    public ApproveJoinChannelSaga(ApproveJoinChannelSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Approved)
        {
            var command = new StartInviteToChannelCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.RequestInfo.UserId,
                0,
                new[] { domainEvent.AggregateEvent.UserId },
                null,
                Array.Empty<long>(),
                DateTime.UtcNow.ToTimestamp(),
                Random.Shared.NextInt64(),
                BitConverter.ToString(new TMessageActionChatJoinedByRequest().ToBytes()).Replace("-", string.Empty),
                ChatJoinType.ApprovedByAdmin
            );
            Publish(command);
        }
        //var updateChatInviteRequestPendingCommand = new UpdateChatInviteRequestPendingCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
        //    domainEvent.AggregateEvent.UserId);
        //Publish(updateChatInviteRequestPendingCommand);


        return CompleteAsync(cancellationToken);
    }
}