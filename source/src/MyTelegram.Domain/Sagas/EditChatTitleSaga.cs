namespace MyTelegram.Domain.Sagas;

public class
    EditChatTitleSaga : MyInMemoryAggregateSaga<EditChatTitleSaga, EditChatTitleSagaId, EditChatTitleSagaLocator>,
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatTitleEditedEvent>
{
    public EditChatTitleSaga(EditChatTitleSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatTitleEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var toPeerId = domainEvent.AggregateEvent.ChatId;
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
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

        var command = new StartSendMessageCommand(
            aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem,
            false,
            1,
            domainEvent.AggregateEvent.CorrelationId
        );
        Publish(command);
        return CompleteAsync(cancellationToken);
    }
}
