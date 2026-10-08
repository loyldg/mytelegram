namespace MyTelegram.Domain.Sagas;

public class
    ImportChatInviteSagaState : AggregateState<ImportChatInviteSaga, ImportChatInviteSagaId, ImportChatInviteSagaState>,
    IApply<ImportChatInviteStartedSagaEvent>,
    IApply<ImportChatInviteCompletedSagaEvent>
{
    public void Apply(ImportChatInviteStartedSagaEvent aggregateEvent)
    {

    }

    public void Apply(ImportChatInviteCompletedSagaEvent aggregateEvent)
    {

    }
}

public class ImportChatInviteStartedSagaEvent : AggregateEvent<ImportChatInviteSaga, ImportChatInviteSagaId> { }
public class ImportChatInviteCompletedSagaEvent(RequestInfo requestInfo, long channelId, bool broadcast) : AggregateEvent<ImportChatInviteSaga, ImportChatInviteSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long ChannelId { get; } = channelId;
    public bool Broadcast { get; } = broadcast;
}

public class ImportChatInviteSaga :
    MyInMemoryAggregateSaga<ImportChatInviteSaga, ImportChatInviteSagaId, ImportChatInviteSagaLocator>,
    ISagaIsStartedBy<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent>,
    ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>
{
    private readonly ImportChatInviteSagaState _state = new();
    private readonly IIdGenerator _idGenerator;

    public ImportChatInviteSaga(ImportChatInviteSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ImportChatInviteStartedSagaEvent());
        if (domainEvent.AggregateEvent.ChatInviteRequestState == ChatInviteRequestState.NoApprovalRequired)
        {
            var inviterId = domainEvent.AggregateEvent.AdminId;
            if (domainEvent.AggregateEvent.IsBroadcast)
            {
                inviterId = domainEvent.AggregateEvent.RequestInfo.UserId;
            }

            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId,
                    domainEvent.AggregateEvent.RequestInfo.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.RequestInfo.UserId,
                inviterId,
                DateTime.UtcNow.ToTimestamp(),
                false, // Bot can only be invited by admin
                domainEvent.AggregateEvent.InviteId,
                domainEvent.AggregateEvent.IsBroadcast,
                ChatJoinType.ByChatInvite
            );
            Publish(command);

        }
        else
        {
            var command = new CreateJoinChannelRequestCommand(
                JoinChannelId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.RequestInfo.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.InviteId
                );
            Publish(command);

            await CompleteAsync(cancellationToken);
        }
    }

    public async Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var incrementParticipantCountCommand = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(incrementParticipantCountCommand);

        if (!domainEvent.AggregateEvent.IsBroadcast)
        {
            var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
            var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
            var ownerPeer = ownerPeerId.ToChannelPeer();
            var senderPeer = domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer();

            var messageItem = new MessageItem(
                ownerPeer,
                ownerPeer,
                senderPeer,
                domainEvent.AggregateEvent.UserId,
                outMessageId,
                string.Empty,
                DateTime.UtcNow.ToTimestamp(),
                Random.Shared.NextInt64(),
                true,
                SendMessageType.MessageService,
                MessageType.Text,
                MessageSubType.ChatJoinByChatInvite,
                MessageAction: new TMessageActionChatJoinedByLink
                {
                    InviterId = domainEvent.AggregateEvent.InviterId
                },
                MessageActionType: MessageActionType.ChatJoinedByChatInvite
            );
            var command = new StartSendMessageCommand(TempId.New,
                domainEvent.AggregateEvent.RequestInfo,
                [new SendMessageItem(messageItem)]);
            Publish(command);
        }
        Emit(new ImportChatInviteCompletedSagaEvent(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.IsBroadcast));

        await CompleteAsync(cancellationToken);
    }
}