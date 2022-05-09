namespace MyTelegram.Domain.Sagas;

public class EditChannelTitleSaga : MyInMemoryAggregateSaga<EditChannelTitleSaga, EditChannelTitleSagaId, EditChannelTitleSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelTitleEditedEvent>
{
    public EditChannelTitleSaga(EditChannelTitleSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelTitleEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.RandomId);
        var ownerPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId);
        var senderPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.Request.UserId);
        var messageItem = new MessageItem(
            ownerPeer,
            ownerPeer,
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
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.Request,
            messageItem,
            correlationId: domainEvent.AggregateEvent.CorrelationId);

        Publish(command);
        return CompleteAsync(cancellationToken);
    }
}
