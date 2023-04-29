namespace MyTelegram.Domain.Sagas;

public class
    EditChannelPhotoSaga : MyInMemoryAggregateSaga<EditChannelPhotoSaga, EditChannelPhotoSagaId, EditChannelPhotoSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelPhotoEditedEvent>
{
    public EditChannelPhotoSaga(EditChannelPhotoSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelPhotoEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.RandomId);
        var ownerPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId);
        var senderPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId);

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
            MessageActionType.ChatEditPhoto
        );
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem,
            correlationId: domainEvent.AggregateEvent.CorrelationId);

        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}
