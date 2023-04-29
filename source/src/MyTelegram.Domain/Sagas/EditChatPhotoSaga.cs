namespace MyTelegram.Domain.Sagas;

public class
    EditChatPhotoSaga : MyInMemoryAggregateSaga<EditChatPhotoSaga, EditChatPhotoSagaId, EditChatPhotoSagaLocator>,
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatPhotoEditedEvent>
{
    public EditChatPhotoSaga(EditChatPhotoSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatPhotoEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
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
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem,
            correlationId: domainEvent.AggregateEvent.CorrelationId);

        Publish(command);
        return CompleteAsync(cancellationToken);
    }
}
