namespace MyTelegram.Domain.Sagas;

public class
    EditChannelPhotoSaga : MyInMemoryAggregateSaga<EditChannelPhotoSaga, EditChannelPhotoSagaId, EditChannelPhotoSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelPhotoEditedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public EditChannelPhotoSaga(EditChannelPhotoSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelPhotoEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, domainEvent.AggregateEvent.ChannelId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(domainEvent.AggregateEvent.ChannelId, outMessageId);
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
            MessageSubType.EditChannelPhoto,
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