using MyTelegram.Domain.Aggregates.ChatInvite;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Domain.Sagas;

public class ImportChatInviteSaga :
    MyInMemoryAggregateSaga<ImportChatInviteSaga, ImportChatInviteSagaId, ImportChatInviteSagaLocator>,
    ISagaIsStartedBy<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent>
{
    public ImportChatInviteSaga(ImportChatInviteSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public async Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.ChatInviteRequestState == ChatInviteRequestState.NotNeedApprove)
        {
            var command = new StartInviteToChannelCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.RequestInfo.UserId,
                0,
                new[] { domainEvent.AggregateEvent.RequestInfo.UserId },
                null,
                Array.Empty<long>(),
                DateTime.UtcNow.ToTimestamp(),
                Random.Shared.NextInt64(),
                BitConverter.ToString(new TMessageActionChatJoinedByLink().ToBytes()).Replace("-", string.Empty),
                ChatJoinType.ByLink
            );
            Publish(command);
        }
        else
        {
            var updateChatInviteRequestPendingCommand = new UpdateChatInviteRequestPendingCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
                domainEvent.AggregateEvent.RequestInfo.UserId);
            Publish(updateChatInviteRequestPendingCommand);
        }

        await CompleteAsync(cancellationToken);
    }
}