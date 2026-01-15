namespace MyTelegram.Domain.Sagas;

public class ApproveJoinChannelSaga : MyInMemoryAggregateSaga<ApproveJoinChannelSaga, ApproveJoinChannelSagaId, ApproveJoinChannelSagaLocator>,
        //ISagaIsStartedBy<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent>,
        ISagaIsStartedBy<JoinChannelAggregate, JoinChannelId, JoinChannelRequestUpdatedEvent>,
        ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>
{
    private readonly ApproveJoinChannelSagaState _state = new();
    public ApproveJoinChannelSaga(ApproveJoinChannelSagaId id, IEventStore eventStore) : base(id,
        eventStore)
    {
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<JoinChannelAggregate, JoinChannelId, JoinChannelRequestUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Approved)
        {
            Emit(new ApproveJoinChannelStartedSagaEvent(
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.TopMessageId,
                domainEvent.AggregateEvent.ChannelHistoryMinId,
                domainEvent.AggregateEvent.Broadcast,
                domainEvent.AggregateEvent.UserId
                ));

            var inviterId = domainEvent.AggregateEvent.UserId;

            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.UserId,
                inviterId,
                DateTime.UtcNow.ToTimestamp(),
                false,
                domainEvent.AggregateEvent.InviteId,
                domainEvent.AggregateEvent.Broadcast,
                ChatJoinType.ByRequest
            );
            Publish(command);
        }
        else
        {
            Emit(new ApproveJoinChannelCompletedSagaEvent(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Approved, domainEvent.AggregateEvent.Broadcast));
            return CompleteAsync(cancellationToken);
        }

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);

        var toPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId);
        var createDialogCommand = new CreateDialogCommand(
            DialogId.Create(domainEvent.AggregateEvent.UserId, toPeer),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.UserId,
            toPeer,
            _state.ChannelHistoryMinId,
            _state.TopMessageId
        );
        Publish(createDialogCommand);

        await HandleJoinChannelCompletedAsync();
        Emit(new ApproveJoinChannelCompletedSagaEvent(_state.RequestInfo, _state.ChannelId, domainEvent.AggregateEvent.UserId, true, domainEvent.AggregateEvent.IsBroadcast));
    }

    private Task HandleJoinChannelCompletedAsync()
    {
        if (!_state.Broadcast)
        {
            var requestInfo = _state.RequestInfo;
            var ownerPeerId = _state.ChannelId;
            var outMessageId = 0;
            var ownerPeer = ownerPeerId.ToChannelPeer();
            //var senderUserId = requestInfo.UserId;
            var senderUserId = _state.UserId;
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
                MessageSubType.ChatJoinByRequest,
                MessageAction: new TMessageActionChatJoinedByRequest(),
                MessageActionType: MessageActionType.ChatJoinByRequest
            );
            var command = new StartSendMessageCommand(TempId.New,
                requestInfo with { IsSubRequest = true },
                [new SendMessageItem(messageItem)]
            );
            Publish(command);
        }

        return CompleteAsync();
    }
}

public class ApproveJoinChannelStartedSagaEvent(RequestInfo requestInfo,
    long channelId,
    int topMessageId,
    int channelHistoryMinId,
    bool broadcast,
    long userId
    ) : AggregateEvent<ApproveJoinChannelSaga, ApproveJoinChannelSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long ChannelId { get; } = channelId;
    public int TopMessageId { get; } = topMessageId;
    public int ChannelHistoryMinId { get; } = channelHistoryMinId;
    public bool Broadcast { get; } = broadcast;
    public long UserId { get; } = userId;
}

public class ApproveJoinChannelCompletedSagaEvent(RequestInfo requestInfo, long channelId, long userId, bool approved, bool broadcast) : RequestAggregateEvent2<ApproveJoinChannelSaga, ApproveJoinChannelSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public long UserId { get; } = userId;
    public bool Approved { get; } = approved;
    public bool Broadcast { get; } = broadcast;
}


public class ApproveJoinChannelSagaState : AggregateState<ApproveJoinChannelSaga, ApproveJoinChannelSagaId,
    ApproveJoinChannelSagaState>,
    IApply<ApproveJoinChannelStartedSagaEvent>,
    IApply<ApproveJoinChannelCompletedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = null!;
    public long ChannelId { get; private set; }
    public int TopMessageId { get; private set; }
    public int ChannelHistoryMinId { get; private set; }
    public bool Broadcast { get; private set; }
    public long UserId { get; private set; }
    public void Apply(ApproveJoinChannelStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
        TopMessageId = aggregateEvent.TopMessageId;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
        Broadcast = aggregateEvent.Broadcast;
        UserId = aggregateEvent.UserId;
    }

    public void Apply(ApproveJoinChannelCompletedSagaEvent aggregateEvent)
    {

    }
}