namespace MyTelegram.Domain.Sagas;

public class
    EditChatTitleSaga : MyInMemoryAggregateSaga<EditChatTitleSaga, EditChatTitleSagaId, EditChatTitleSagaLocator>,
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatTitleEditedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public EditChatTitleSaga(EditChatTitleSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatTitleEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var toPeerId = domainEvent.AggregateEvent.ChatId;
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var ownerPeer = new Peer(PeerType.User, ownerPeerId);
        var toPeer = new Peer(PeerType.Chat, toPeerId);
        var senderPeer = new Peer(PeerType.User, ownerPeerId);
        var messageItem = new MessageItem(
            ownerPeer,
            toPeer,
            senderPeer,
            outMessageId,
            string.Empty,
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.Normal,
            null,
            domainEvent.AggregateEvent.MessageActionData,
            MessageActionType.ChatEditTitle
        );

        var command = new CreateOutboxMessageCommand(
            aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem
        );
        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}
