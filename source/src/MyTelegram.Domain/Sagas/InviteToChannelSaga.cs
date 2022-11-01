namespace MyTelegram.Domain.Sagas;

public class InviteToChannelSaga :
    MyInMemoryAggregateSaga<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
    ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    IApply<InviteToChannelCompletedEvent>
{
    private readonly InviteToChannelSagaState _state = new();

    public InviteToChannelSaga(InviteToChannelSagaId id, IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }

    public void Apply(InviteToChannelCompletedEvent aggregateEvent)
    {
        CompleteAsync();
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaMemberCreatedEvent());
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);

        if (!domainEvent.AggregateEvent.IsRejoin)
        {
            var toPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId);
            var createDialogCommand = new CreateDialogCommand(
                DialogId.Create(domainEvent.AggregateEvent.UserId, toPeer),
                domainEvent.AggregateEvent.UserId,
                toPeer,
                _state.ChannelHistoryMinId,
                _state.MaxMessageId,
                //topMessageBoxId,
                _state.CorrelationId
            );
            Publish(createDialogCommand);
        }

        await HandleInviteToChannelCompletedAsync().ConfigureAwait(false);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaStartEvent(
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.InviterId,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.MemberUidList.Count,
            domainEvent.AggregateEvent.MemberUidList,
            domainEvent.AggregateEvent.MaxMessageId,
            domainEvent.AggregateEvent.ChannelHistoryMinId,
            domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.MessageActionData,
            domainEvent.AggregateEvent.Broadcast,
            domainEvent.AggregateEvent.CorrelationId
        ));
        foreach (var userId in domainEvent.AggregateEvent.MemberUidList)
        {
            var isBot = domainEvent.AggregateEvent.BotUidList.Contains(userId);
            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, userId),
                domainEvent.AggregateEvent.ChannelId,
                userId,
                domainEvent.AggregateEvent.InviterId,
                domainEvent.AggregateEvent.Date,
                isBot,
                domainEvent.AggregateEvent.CorrelationId
            );
            Publish(command);
        }

        return Task.CompletedTask;
    }

    private Task HandleInviteToChannelCompletedAsync()
    {
        if (_state.Completed)
        {
            // send service message to member after invited to channel
            if (!_state.Broadcast)
            {
            var ownerPeerId = _state.ChannelId;
            var outMessageId = 0;
            var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, _state.RandomId);
            var ownerPeer = new Peer(PeerType.Channel, ownerPeerId);
            var senderPeer = new Peer(PeerType.User, _state.InviterId);

            var command = new StartSendMessageCommand(
                aggregateId,
                _state.RequestInfo,
                new MessageItem(
                    ownerPeer,
                    ownerPeer,
                    senderPeer,
                    outMessageId,
                    string.Empty,
                    DateTime.UtcNow.ToTimestamp(),
                    _state.RandomId,
                    true,
                    SendMessageType.MessageService,
                    MessageType.Text,
                    MessageSubType.InviteToChannel,
                    null,
                    _state.MessageActionData,
                    MessageActionType.ChatAddUser
                ),
                false,
                1,
                _state.CorrelationId
            );

            Publish(command);
            }

            Emit(new InviteToChannelCompletedEvent(_state.RequestInfo.ReqMsgId,
                _state.ChannelId,
                _state.InviterId,
                _state.MemberUidList,
                _state.CorrelationId));
        }

        return Task.CompletedTask;
    }
}
