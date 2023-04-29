namespace MyTelegram.Domain.Sagas;

public class AddChatUserSaga : MyInMemoryAggregateSaga<AddChatUserSaga, AddChatUserSagaId, AddChatUserSagaLocator>,
    ISagaIsStartedBy<ChatAggregate, ChatId, ChatMemberAddedEvent>
{
    public AddChatUserSaga(AddChatUserSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.ChatMember.InviterId;
        var ownerPeer = new Peer(PeerType.User, ownerPeerId);
        //var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            new MessageItem(
                ownerPeer,
                new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
                ownerPeer,
                0,
                string.Empty,
                domainEvent.AggregateEvent.ChatMember.Date,
                domainEvent.AggregateEvent.RandomId,
                true,
                SendMessageType.MessageService,
                MessageType.Text,
                MessageSubType.AddChatUser,
                null,
                domainEvent.AggregateEvent.MessageActionData,
                MessageActionType.ChatAddUser
            ),
            false,
            1,
            Guid.NewGuid()
        );

        Publish(command);

        await CompleteAsync(cancellationToken);
    }
}
