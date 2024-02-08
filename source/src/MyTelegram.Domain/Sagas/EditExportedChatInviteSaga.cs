namespace MyTelegram.Domain.Sagas;

public class EditExportedChatInviteSaga : MyInMemoryAggregateSaga<EditExportedChatInviteSaga, EditExportedChatInviteSagaId, EditExportedChatInviteSagaLocator>,
    ISagaIsStartedBy<ChatInviteAggregate, ChatInviteId, ChatInviteEditedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public EditExportedChatInviteSaga(EditExportedChatInviteSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteEditedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Revoked)
        {
            var inviteId = await _idGenerator.NextLongIdAsync(IdType.InviteId, domainEvent.AggregateEvent.ChannelId, cancellationToken: cancellationToken);
            var command = new CreateChatInviteCommand(
                ChatInviteId.Create(domainEvent.AggregateEvent.ChannelId, inviteId),
                domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
                domainEvent.AggregateEvent.ChannelId,
                inviteId,
                domainEvent.AggregateEvent.NewHash!,
                domainEvent.AggregateEvent.AdminId,
                domainEvent.AggregateEvent.Title,
                domainEvent.AggregateEvent.RequestNeeded,
                domainEvent.AggregateEvent.StartDate,
                domainEvent.AggregateEvent.ExpireDate,
                domainEvent.AggregateEvent.UsageLimit,
                domainEvent.AggregateEvent.Permanent,
                DateTime.UtcNow.ToTimestamp()
            );
            Publish(command);
        }

        await CompleteAsync(cancellationToken);
    }
}