namespace MyTelegram.Domain.Sagas;

public class CreateChannelSaga : MyInMemoryAggregateSaga<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelCreatedEvent>
{
    //private readonly CreateChannelSagaState _state = new();
    public CreateChannelSaga(CreateChannelSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
        //Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
        var messageItem = new MessageItem(
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.User, domainEvent.AggregateEvent.CreatorId),
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.CreateChannel,
            null,
            domainEvent.AggregateEvent.MessageActionData,
            MessageActionType.ChannelCreate
        );
        var command = new StartSendMessageCommand(aggregateId, domainEvent.AggregateEvent.RequestInfo, messageItem, correlationId: Guid.NewGuid());

        Publish(command);

        var createMemberCommand = new CreateChannelCreatorMemberCommand(
            ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CreatorId),
            domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.CreatorId,
            domainEvent.AggregateEvent.Date);
        Publish(createMemberCommand);
        await CompleteAsync(cancellationToken);
    }
}
