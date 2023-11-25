namespace MyTelegram.Domain.Sagas;

public class DeleteChatUserSaga : MyInMemoryAggregateSaga<DeleteChatUserSaga, DeleteChatUserSagaId, DeleteChatUserSagaLocator>,
    ISagaIsStartedBy<ChatAggregate, ChatId, ChatMemberDeletedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public DeleteChatUserSaga(DeleteChatUserSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId,ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
            new Peer(PeerType.User, ownerPeerId),
            outMessageId,
            string.Empty,
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.DeleteChatUser,
            messageActionData: domainEvent.AggregateEvent.MessageActionData,
            messageActionType: MessageActionType.ChatDeleteUser
        );
        var command = new CreateOutboxMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid() },
            messageItem);

        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}
