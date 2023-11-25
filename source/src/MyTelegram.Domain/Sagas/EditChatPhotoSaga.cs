namespace MyTelegram.Domain.Sagas;

public class
    EditChatPhotoSaga : MyInMemoryAggregateSaga<EditChatPhotoSaga, EditChatPhotoSagaId, EditChatPhotoSagaLocator>,
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatPhotoEditedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public EditChatPhotoSaga(EditChatPhotoSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatPhotoEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var ownerPeer = new Peer(PeerType.User, ownerPeerId);
        var toPeer = new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId);
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
            MessageActionType.ChatEditPhoto
        );
        var command = new CreateOutboxMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem);

        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}
