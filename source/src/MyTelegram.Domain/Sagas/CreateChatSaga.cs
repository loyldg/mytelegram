namespace MyTelegram.Domain.Sagas;

public class CreateChatSaga : MyInMemoryAggregateSaga<CreateChatSaga, CreateChatSagaId, CreateChatSagaLocator>,
    ISagaIsStartedBy<ChatAggregate, ChatId, ChatCreatedEvent>
{
    public CreateChatSaga(CreateChatSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.CreatorUid;
        var chatId = domainEvent.AggregateEvent.ChatId;
        var outMessageId = 0;

        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.Chat, chatId),
            new Peer(PeerType.User, ownerPeerId),
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.CreateChat,
            null,
            domainEvent.AggregateEvent.MessageActionData,
            MessageActionType.ChatCreate
        );
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem,
            correlationId: domainEvent.AggregateEvent.CorrelationId);
        Publish(command);
        await CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
}
