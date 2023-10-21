namespace MyTelegram.Domain.Sagas;

public class CreateChannelSaga : MyInMemoryAggregateSaga<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaLocator>,
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
        ISagaHandles<ChannelAggregate, ChannelId, ChannelInviteExportedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly CreateChannelSagaState _state = new();

    //private readonly CreateChannelSagaState _state = new();
    public CreateChannelSaga(CreateChannelSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new CreateChannelSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.MessageActionData, domainEvent.AggregateEvent.RandomId, domainEvent.AggregateEvent.MigratedFromChat));
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        // increment pts(read channel inbox)
        await _idGenerator.NextIdAsync(IdType.Pts, ownerPeerId, cancellationToken: cancellationToken);

        // create dialog for creator
        var createDialogCommand = new CreateDialogCommand(
            DialogId.Create(domainEvent.AggregateEvent.RequestInfo.UserId,
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId)), domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.CreatorId,
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId),
            0,
            1
            );
        Publish(createDialogCommand);

        // add creator to channel member list
        var createMemberCommand = new CreateChannelCreatorMemberCommand(
            ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CreatorId),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.CreatorId,
            domainEvent.AggregateEvent.Date);
        Publish(createMemberCommand);

        // export default chat invite after channel created
        var chatInvitedId = await _idGenerator.NextLongIdAsync(IdType.InviteId, ownerPeerId, cancellationToken: cancellationToken);
        var bytes = new byte[12];
        Random.Shared.NextBytes(bytes);
        var inviteHash = $"{Convert.ToBase64String(bytes)
            .Replace($"+", "/")
            .Replace("=", string.Empty)}";
        var exportChatInviteCommand = new ExportChatInviteCommand(ChannelId.Create(ownerPeerId),
            domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
            domainEvent.AggregateEvent.RequestInfo.UserId,
            chatInvitedId,
            domainEvent.AggregateEvent.Title,
            false,
            null,
            null,
            true,
            inviteHash,
            DateTime.UtcNow.ToTimestamp()
        );
        Publish(exportChatInviteCommand);

        //await CompleteAsync(cancellationToken);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteExportedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.ChannelId;
        var creatorId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var messageItem = new MessageItem(
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.Channel, ownerPeerId),
            new Peer(PeerType.User, creatorId),
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.Date,
            //domainEvent.AggregateEvent.RandomId,
            _state.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            _state.MigratedFromChat ? MessageSubType.MigrateChat : MessageSubType.CreateChannel,
            null,
            _state.MessageActionData,
            MessageActionType.ChannelCreate
        );
        //var command = new StartSendMessageCommand(aggregateId, domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid() }, messageItem);
        var command = new CreateOutboxMessageCommand(aggregateId, _state.RequestInfo, messageItem);

        Publish(command);

        await CompleteAsync(cancellationToken);
    }
}
