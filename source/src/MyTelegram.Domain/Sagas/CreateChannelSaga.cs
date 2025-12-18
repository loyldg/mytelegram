namespace MyTelegram.Domain.Sagas;

public class CreateChannelSaga :
    MyInMemoryAggregateSaga<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISagaHandles<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly IChatInviteLinkHelper _chatInviteLinkHelper;
    private readonly CreateChannelSagaState _state = new();

    public CreateChannelSaga(CreateChannelSagaId id, IEventStore eventStore, IIdGenerator idGenerator, IChatInviteLinkHelper chatInviteLinkHelper) : base(id,
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
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var subType = _state.MigratedFromChat ? MessageSubType.MigrateChat : MessageSubType.CreateChannel;
        if (_state.AutoCreateFromChat)
        {
            subType = MessageSubType.AutoCreateChannelFromChat;
        }
        var messageItem = new MessageItem(
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.User, creatorId),
            creatorId,
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.Date,
            _state.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            subType,
            null,
            //_state.MessageActionData,
            MessageActionType: MessageActionType.ChannelCreate,
            MessageAction: new TMessageActionChannelCreate
            {
                Title = _state.Title
            },
            Post: _state.Broadcast//,
                                  //TtlPeriod: _state.TtlPeriod,
                                  //IsTtlFromDefaultSetting: _state.IsTtlFromDefaultSetting
        );

        var command = new StartSendMessageCommand(TempId.New,
            _state.RequestInfo,
            [new SendMessageItem(messageItem)]
        );

        Publish(command);

        await CompleteAsync(cancellationToken);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new CreateChannelSagaStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.Title,
            domainEvent.AggregateEvent.Broadcast,
            domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.MigratedFromChat,
            domainEvent.AggregateEvent.AutoCreateFromChat,
            domainEvent.AggregateEvent.TtlPeriod,
            domainEvent.AggregateEvent.TtlFromDefaultSetting,
            domainEvent.AggregateEvent.MemberUserIds,
            domainEvent.AggregateEvent.BotUserIds
            ));
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        await _idGenerator.NextIdAsync(IdType.Pts, ownerPeerId, cancellationToken: cancellationToken);

        //// Create dialog for creator
        //var createDialogCommand = new CreateDialogCommand(
        //    DialogId.Create(domainEvent.AggregateEvent.RequestInfo.UserId,
        //        new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId)),
        //    domainEvent.AggregateEvent.RequestInfo,
        //    domainEvent.AggregateEvent.CreatorId,
        //    new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId),
        //    0,
        //    1
        //);
        //Publish(createDialogCommand);

        // Add creator to channel member list
        var createMemberCommand = new CreateChannelCreatorCommand(
            ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CreatorId),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.CreatorId,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.Broadcast
            );
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
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.Broadcast
        );
        Publish(createChatInviteCommand);

        if (domainEvent.AggregateEvent.MemberUserIds.Count > 0)
        {
            var command = new StartInviteToChannelCommand(TempId.New,
                domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid(), ReqMsgId = 0 },
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.Broadcast,
                false,
                domainEvent.AggregateEvent.RequestInfo.UserId,
                0,
                1,
                domainEvent.AggregateEvent.MemberUserIds,
                domainEvent.AggregateEvent.BotUserIds,
                ChatJoinType.InvitedByAdmin
            );
            Publish(command);
        }
    }
}