namespace MyTelegram.Domain.Sagas;

public class DeleteChatUserSaga : MyInMemoryAggregateSaga<DeleteChatUserSaga, DeleteChatUserSagaId, DeleteChatUserSagaLocator>,
    ISagaIsStartedBy<ChatAggregate, ChatId, ChatMemberDeletedEvent>
{
    public DeleteChatUserSaga(DeleteChatUserSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
    }

    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = 0;
        var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, domainEvent.AggregateEvent.RandomId);
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
            new Peer(PeerType.User, ownerPeerId),
            outMessageId,
            string.Empty,
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.DeleteChatUser,
            messageActionData: domainEvent.AggregateEvent.MessageActionData,
            messageActionType: MessageActionType.ChatDeleteUser
        );
        var command = new StartSendMessageCommand(aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem,
            correlationId: Guid.NewGuid());

        Publish(command);
        await CompleteAsync(cancellationToken).ConfigureAwait(false);
    }
}
