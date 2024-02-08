namespace MyTelegram.Domain.Sagas;

public class CreateChannelSaga :
    MyInMemoryAggregateSaga<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISagaHandles<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly IChatInviteLinkHelper _chatInviteLinkHelper;
    private readonly CreateChannelSagaState _state = new();

    public CreateChannelSaga(CreateChannelSagaId id, IEventStore eventStore, IIdGenerator idGenerator,IChatInviteLinkHelper chatInviteLinkHelper) : base(id,
        eventStore)
    {
        _idGenerator = idGenerator;
        _chatInviteLinkHelper = chatInviteLinkHelper;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent> domainEvent,
        ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        var creatorId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId =
            await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var messageItem = new MessageItem(
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.User, creatorId),
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.Date,
            _state.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            _state.MigratedFromChat ? MessageSubType.MigrateChat : MessageSubType.CreateChannel,
            null,
            _state.MessageActionData,
            MessageActionType.ChannelCreate
        );
        var command = new CreateOutboxMessageCommand(aggregateId, _state.RequestInfo, messageItem);

        Publish(command);

        await CompleteAsync(cancellationToken);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new CreateChannelSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.MessageActionData, domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.MigratedFromChat));
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        await _idGenerator.NextIdAsync(IdType.Pts, ownerPeerId, cancellationToken: cancellationToken);

        // Create dialog for creator
        var createDialogCommand = new CreateDialogCommand(
            DialogId.Create(domainEvent.AggregateEvent.RequestInfo.UserId,
                new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId)),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.CreatorId,
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId),
            0,
            1
        );
        Publish(createDialogCommand);

        // Add creator to channel member list
        var createMemberCommand = new CreateChannelCreatorMemberCommand(
            ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CreatorId),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.CreatorId,
            domainEvent.AggregateEvent.Date);
        Publish(createMemberCommand);

        // Export default chat invite after channel created
        var chatInvitedId =
            await _idGenerator.NextLongIdAsync(IdType.InviteId, ownerPeerId, cancellationToken: cancellationToken);
        var inviteHash = _chatInviteLinkHelper.GenerateInviteLink();

        var createChatInviteCommand = new CreateChatInviteCommand(ChatInviteId.Create(ownerPeerId, chatInvitedId),
            domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
            ownerPeerId,
            chatInvitedId,
            inviteHash,
            domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.Title,
            false,
            null,
            null,
            null,
            true,
            DateTime.UtcNow.ToTimestamp()
        );
        Publish(createChatInviteCommand);
    }
}