namespace MyTelegram.Domain.Sagas;

public class InviteToChannelSaga :
    MyInMemoryAggregateSaga<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaLocator>,
    ISagaIsStartedBy<TempAggregate, TempId, InviteToChannelStartedEvent>,
    ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>
{
    private readonly InviteToChannelSagaState _state = new();

    public InviteToChannelSaga(InviteToChannelSagaId id, IEventStore eventStore) : base(id,
        eventStore)
    {
        Register(_state);
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaMemberCreatedSagaEvent());
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);

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

        await HandleInviteToChannelCompletedAsync();
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, InviteToChannelStartedEvent> domainEvent,
        ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaStartSagaEvent(
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.IsBroadcast,
            domainEvent.AggregateEvent.HasLink,
            domainEvent.AggregateEvent.InviterId,
            domainEvent.AggregateEvent.MemberUserIds,
            domainEvent.AggregateEvent.BotUserIds,
            domainEvent.AggregateEvent.ChannelHistoryMinId,
            domainEvent.AggregateEvent.MaxMessageId,
            domainEvent.AggregateEvent.ChatJoinType
        ));

        var date = DateTime.UtcNow.ToTimestamp();
        foreach (var userId in domainEvent.AggregateEvent.MemberUserIds)
        {
            var isBot = domainEvent.AggregateEvent.BotUserIds.Contains(userId);
            CreateChannelMember(_state.RequestInfo, domainEvent.AggregateEvent.ChannelId,
                userId,
                domainEvent.AggregateEvent.RequestInfo.UserId,
                isBot,
                domainEvent.AggregateEvent.IsBroadcast,
                date
            );
        }

        return Task.CompletedTask;
    }

    private void CreateChannelMember(RequestInfo requestInfo, long channelId, long userId, long inviterUserId,
        bool isBot, bool isBroadcast, int date)
    {
        var command = new CreateChannelMemberCommand(
            ChannelMemberId.Create(channelId, userId),
            requestInfo,
            channelId,
            userId,
            inviterUserId,
            date,
            isBot,
            null,
            isBroadcast
        );
        Publish(command);
    }

    private Task HandleInviteToChannelCompletedAsync()
    {
        if (_state.Completed)
        {
            if (!_state.Broadcast)
            {
                var ownerPeerId = _state.ChannelId;
                var outMessageId = 0;
                var ownerPeer = new Peer(PeerType.Channel, ownerPeerId);
                var senderPeer = new Peer(PeerType.User, _state.InviterId);

                List<long> allMemberUserIds = [];
                allMemberUserIds.AddRange(_state.MemberUserIds);
                allMemberUserIds.AddRange(_state.BotUserIds);

                var messageSubType = MessageSubType.None;
                Peer? sendAs = null;
                switch (_state.ChatJoinType)
                {
                    case ChatJoinType.InvitedByAdmin:
                        messageSubType = MessageSubType.InviteToChannel;
                        if (_state is { HasLink: true, Broadcast: false })
                        {
                            sendAs = _state.ChannelId.ToChannelPeer();
                        }
                        break;
                    case ChatJoinType.BySelf:
                        messageSubType = MessageSubType.ChatJoinByRequest;
                        break;
                    case ChatJoinType.ByChatInvite:
                        messageSubType = MessageSubType.ChatJoinByChatInvite;
                        break;
                    case ChatJoinType.ByRequest:
                        messageSubType = MessageSubType.ChatJoinByRequest;
                        break;
                }

                var messageItem = new MessageItem(
                    ownerPeer,
                    ownerPeer,
                    senderPeer,
                    _state.InviterId,
                    outMessageId,
                    string.Empty,
                    DateTime.UtcNow.ToTimestamp(),
                    _state.RandomId,
                    true,
                    SendMessageType.MessageService,
                    MessageType.Text,
                    messageSubType,
                    null,
                    new TMessageActionChatAddUser
                    {
                        Users = new TVector<long>(allMemberUserIds)
                    },
                    MessageActionType.ChatAddUser,
                    SendAs: sendAs
                );
                var command = new StartSendMessageCommand(TempId.New,
                    _state.RequestInfo with { IsSubRequest = true },
                    [new SendMessageItem(messageItem)]);

                Publish(command);
            }

            Emit(new InviteToChannelCompletedSagaEvent(_state.RequestInfo,
                _state.ChannelId,
                _state.InviterId,
                _state.Broadcast,
                _state.MemberUserIds,
                _state.BotUserIds,
                _state.HasLink,
                _state.ChatJoinType
            ));

            return CompleteAsync();
        }

        return Task.CompletedTask;
    }
}