namespace MyTelegram.Domain.Sagas;

public class InviteToChannelSaga :
    MyInMemoryAggregateSaga<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
    ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    IApply<InviteToChannelCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly InviteToChannelSagaState _state = new();

    public InviteToChannelSaga(InviteToChannelSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
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
                _state.RequestInfo,
                domainEvent.AggregateEvent.UserId,
                toPeer,
                _state.ChannelHistoryMinId,
                _state.MaxMessageId
            );
            Publish(createDialogCommand);
        }

        await HandleInviteToChannelCompletedAsync();
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
            domainEvent.AggregateEvent.PrivacyRestrictedUserId,
            domainEvent.AggregateEvent.MaxMessageId,
            domainEvent.AggregateEvent.ChannelHistoryMinId,
            domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.MessageActionData,
            domainEvent.AggregateEvent.Broadcast
        ));
        foreach (var userId in domainEvent.AggregateEvent.MemberUidList)
        {
            var isBot = domainEvent.AggregateEvent.BotUidList.Contains(userId);
            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, userId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                userId,
                domainEvent.AggregateEvent.InviterId,
                domainEvent.AggregateEvent.Date,
                isBot,
                null
            );
            Publish(command);
        }

        return Task.CompletedTask;
    }

    private async Task HandleInviteToChannelCompletedAsync()
    {
        if (_state.Completed)
        {
            // send service message to member after invited to super group
            if (!_state.Broadcast)
            {
                var ownerPeerId = _state.ChannelId;
                var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId);
                var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
                var ownerPeer = new Peer(PeerType.Channel, ownerPeerId);
                var senderPeer = new Peer(PeerType.User, _state.InviterId);

                var command = new CreateOutboxMessageCommand(
                    aggregateId,
                    _state.RequestInfo with { RequestId = Guid.NewGuid() },
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
                    )
                );

                Publish(command);
            }

            Emit(new InviteToChannelCompletedEvent(_state.RequestInfo,
                _state.ChannelId,
                _state.InviterId,
                _state.Broadcast,
                _state.MemberUidList,
                _state.PrivacyRestrictedUserId));
        }
    }
}
