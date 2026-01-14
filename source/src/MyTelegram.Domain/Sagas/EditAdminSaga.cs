namespace MyTelegram.Domain.Sagas;

public class EditAdminSaga : MyInMemoryAggregateSaga<EditAdminSaga, EditAdminSagaId, EditAdminSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
        ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly IChatInviteLinkHelper _chatInviteLinkHelper;
    private readonly EditAdminSagaState _state = new();

    public EditAdminSaga(EditAdminSagaId id,
        IEventStore eventStore, IIdGenerator idGenerator, IChatInviteLinkHelper chatInviteLinkHelper) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        _chatInviteLinkHelper = chatInviteLinkHelper;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new EditAdminStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.IsBroadcast,
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.IsBot,
            domainEvent.AggregateEvent.IsNewAdmin,
            domainEvent.AggregateEvent.PromotedBy,
            domainEvent.AggregateEvent.AdminRights,
            domainEvent.AggregateEvent.Rank
            ));
        if (!domainEvent.AggregateEvent.IsChannelMember)
        {
            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.UserId,
                domainEvent.AggregateEvent.PromotedBy,
                DateTime.UtcNow.ToTimestamp(),
                domainEvent.AggregateEvent.IsBot,
                null,
                domainEvent.AggregateEvent.IsBroadcast
                );
            Publish(command);

            var incrementMemberCountCommand = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
            Publish(incrementMemberCountCommand);
        }
        else
        {
            var command = new EditChannelAdminCommand2(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.UserId,
                _state.AdminRights.GetFlags().ToInt32(),
                _state.Rank
            );
            Publish(command);

            HandleEditAdminCompleted();
        }

        if (domainEvent.AggregateEvent.ShouldCreatePermanentChatInvite)
        {
            // Create permanent chat invites for new admin
            var chatInviteId = await _idGenerator.NextLongIdAsync(IdType.InviteId, domainEvent.AggregateEvent.ChannelId, cancellationToken: cancellationToken);
            var createChatInviteCommand = new CreateChatInviteCommand(
                ChatInviteId.Create(domainEvent.AggregateEvent.ChannelId, chatInviteId),
                RequestInfo.Empty with { UserId = domainEvent.AggregateEvent.RequestInfo.UserId },
                domainEvent.AggregateEvent.ChannelId,
                chatInviteId,
                _chatInviteLinkHelper.GenerateInviteLink(),
                domainEvent.AggregateEvent.UserId,
                string.Empty,
                false,
                null,
                null,
                null,
                true,
                DateTime.UtcNow.ToTimestamp(),
                domainEvent.AggregateEvent.IsBroadcast
            );
            Publish(createChatInviteCommand);
        }
    }

    private void HandleEditAdminCompleted()
    {
        Emit(new EditAdminCompletedSagaEvent(_state.RequestInfo, _state.ChannelId, _state.IsBroadcast, _state.UserId, _state.IsBot, _state.IsNewAdmin, _state.PromotedBy, _state.AdminRights));
        Complete();
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var command = new EditChannelAdminCommand2(
            ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId),
            domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.UserId, _state.AdminRights.GetFlags().ToInt32(),
            _state.Rank
        );
        Publish(command);

        HandleEditAdminCompleted();

        return Task.CompletedTask;
    }
}

public class EditAdminSagaState : AggregateState<EditAdminSaga, EditAdminSagaId, EditAdminSagaState>,
    IApply<EditAdminStartedSagaEvent>,
    IApply<EditAdminCompletedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;
    public long ChannelId { get; private set; }
    public bool IsBroadcast { get; private set; }
    public long UserId { get; private set; }
    public bool IsBot { get; private set; }
    public bool IsNewAdmin { get; private set; }
    public long PromotedBy { get; private set; }
    public ChatAdminRights AdminRights { get; private set; } = default!;
    public string Rank { get; private set; }
    public void Apply(EditAdminStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
        IsBroadcast = aggregateEvent.IsBroadcast;
        UserId = aggregateEvent.UserId;
        IsBot = aggregateEvent.IsBot;
        IsNewAdmin = aggregateEvent.IsNewAdmin;
        PromotedBy = aggregateEvent.PromotedBy;
        AdminRights = aggregateEvent.AdminRights;
        Rank = aggregateEvent.Rank;
    }

    public void Apply(EditAdminCompletedSagaEvent aggregateEvent)
    {

    }
}

public class EditAdminStartedSagaEvent(RequestInfo requestInfo,
    long channelId,
    bool isBroadcast,
    long userId,
    bool isBot,
    bool isNewAdmin,
    long promotedBy,
    ChatAdminRights adminRights,
    string rank
) : RequestAggregateEvent2<EditAdminSaga, EditAdminSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public bool IsBroadcast { get; } = isBroadcast;
    public long UserId { get; } = userId;
    public bool IsBot { get; } = isBot;
    public bool IsNewAdmin { get; } = isNewAdmin;
    public long PromotedBy { get; } = promotedBy;
    public ChatAdminRights AdminRights { get; } = adminRights;
    public string Rank { get; } = rank;
}

public class EditAdminCompletedSagaEvent(RequestInfo requestInfo,
    long channelId,
    bool isBroadcast,
    long userId,
    bool isBot,
    bool isNewAdmin,
    long promotedBy,
    ChatAdminRights adminRights
    ) : RequestAggregateEvent2<EditAdminSaga, EditAdminSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public bool IsBroadcast { get; } = isBroadcast;
    public long UserId { get; } = userId;
    public bool IsBot { get; } = isBot;
    public bool IsNewAdmin { get; } = isNewAdmin;
    public long PromotedBy { get; } = promotedBy;
    public ChatAdminRights AdminRights { get; } = adminRights;
}