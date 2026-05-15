namespace MyTelegram.Domain.Sagas;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<LeaveChannelSagaId>))]
public class LeaveChannelSagaId(string value) : SingleValueObject<string>(value), ISagaId;

public class LeaveChannelSagaLocator : DefaultSagaLocator<LeaveChannelSaga, LeaveChannelSagaId>
{
    protected override LeaveChannelSagaId CreateSagaId(string requestId)
    {
        return new LeaveChannelSagaId(requestId);
    }
}

public class LeaveChannelSaga(LeaveChannelSagaId id, IEventStore eventStore) : MyInMemoryAggregateSaga<LeaveChannelSaga, LeaveChannelSagaId, LeaveChannelSagaLocator>(id, eventStore),
    ISagaIsStartedBy<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var updateParticipantCountCommand = new UpdateParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId), -1);
        Publish(updateParticipantCountCommand);

        if (!domainEvent.AggregateEvent.Broadcast)
        {
            var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
            var outMessageId = 0;
            var ownerPeer = ownerPeerId.ToChannelPeer();
            var senderUserId = domainEvent.AggregateEvent.MemberUserId;
            var senderPeer = senderUserId.ToUserPeer();

            var messageItem = new MessageItem(
                ownerPeer,
                ownerPeer,
                senderPeer,
                senderUserId,
                outMessageId,
                string.Empty,
                DateTime.UtcNow.ToTimestamp(),
                Random.Shared.NextInt64(),
                true,
                SendMessageType.MessageService,
                MessageType.Text,
                MessageSubType.DeleteChatUser,
                MessageAction: new TMessageActionChatDeleteUser
                {
                    UserId = domainEvent.AggregateEvent.MemberUserId
                },
                MessageActionType: MessageActionType.ChatDeleteUser
            );
            var command = new StartSendMessageCommand(TempId.New,
                domainEvent.AggregateEvent.RequestInfo with { IsSubRequest = true },
                [new SendMessageItem(messageItem)]
            );
            Publish(command);
        }

        CompleteAsync();
        return Task.CompletedTask;
    }
}
