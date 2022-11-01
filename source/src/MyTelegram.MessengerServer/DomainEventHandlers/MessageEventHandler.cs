namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class MessageEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent>,

    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>

{
    private readonly IChatEventCacheHelper _chatEventCacheHelper;
    private readonly ILogger<MessageEventHandler> _logger;
    private readonly ITlMessageConverter _messageConverter;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlUpdatesConverter _updatesConverter;
    public MessageEventHandler(
        IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        ITlUpdatesConverter updatesConverter,
        ITlMessageConverter messageConverter,
        IChatEventCacheHelper chatEventCacheHelper,
        ILogger<MessageEventHandler> logger,
        IQueryProcessor queryProcessor) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _updatesConverter = updatesConverter;
        _messageConverter = messageConverter;
        _chatEventCacheHelper = chatEventCacheHelper;
        _logger = logger;
        _queryProcessor = queryProcessor;
    }

    public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return domainEvent.AggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.CreateChannel => HandleCreateChannelAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.InviteToChannel => HandleInviteToChannelAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            _ => HandleSendMessageAsync(domainEvent.AggregateEvent),
        };
    }

    public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return domainEvent.AggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(domainEvent.AggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(domainEvent.AggregateEvent),
            MessageSubType.ForwardMessage => HandleForwardMessageAsync(domainEvent.AggregateEvent),
            _ => HandleReceiveMessageAsync(domainEvent.AggregateEvent)
        };
    }

    public async Task HandleAsync(IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updates =
            _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            updates,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.ToPeer.PeerType
        ).ConfigureAwait(false);
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
            updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.Pts,
            ptsType: PtsType.NewMessages).ConfigureAwait(false);

        // Channel message shares the same message,edit out message should notify channel member
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            var channelEditUpdates = _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent, 0);
            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                channelEditUpdates,
                pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
        }
    }

    public Task HandleAsync(IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        //var toPeer = domainEvent.AggregateEvent.ToPeer with { PeerId = domainEvent.AggregateEvent.OwnerPeerId };
        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId);
        return PushUpdatesToPeerAsync(toPeer,
            _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent),
            pts: domainEvent.AggregateEvent.Pts,
            ptsType: PtsType.NewMessages
        );
    }

    
    private async Task HandleCreateChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_chatEventCacheHelper.TryRemoveChannelCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out ChannelCreatedEvent? eventData))
        {
            var updates = _updatesConverter.ToCreateChannelUpdates(eventData, aggregateEvent);
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo.ReqMsgId,
                updates,
                sourceId,
                aggregateEvent.MessageItem.SenderPeer.PeerId
            ).ConfigureAwait(false);
            await PushUpdatesToChannelSingleMemberAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts
            ).ConfigureAwait(false);
        }
        else
        {
            _logger.LogWarning("Can not find create channel cache info,channelId={ChannelId}",
                aggregateEvent.MessageItem.ToPeer.PeerId);
        }
    }

    private async Task HandleCreateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var updates = _updatesConverter.ToCreateChatUpdates(eventData, aggregateEvent);
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo.ReqMsgId,
                updates,
                sourceId,
                pts: aggregateEvent.Pts).ConfigureAwait(false);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts
            ).ConfigureAwait(false);
        }
    }

    private Task HandleCreateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (_chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var updates = _updatesConverter.ToCreateChatUpdates(eventData, aggregateEvent);

            return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts
            );
        }

        return Task.CompletedTask;
    }

    private Task HandleForwardMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToInboxForwardMessageUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer, updates, pts: aggregateEvent.Pts);
    }

    private async Task HandleInviteToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_chatEventCacheHelper.TryRemoveStartInviteToChannelEvent(aggregateEvent.MessageItem.ToPeer.PeerId,
                out StartInviteToChannelEvent? startInviteToChannelEvent))
        {
            var item = aggregateEvent.MessageItem;
            // TODO: Create channel info from domain events
            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(item.ToPeer.PeerId), default).ConfigureAwait(false);

            var updates = _updatesConverter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel,
                true);
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo.ReqMsgId,
                updates,
                sourceId,
                item.SenderPeer.PeerId).ConfigureAwait(false);
            // notify self other devices
            await PushUpdatesToChannelSingleMemberAsync(item.SenderPeer, updates, aggregateEvent.RequestInfo.AuthKeyId)
                .ConfigureAwait(false);
            var updatesForChannelMember = _updatesConverter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel,
                false);
            // notify channel members
            await PushUpdatesToChannelMemberAsync(item.ToPeer,
                updatesForChannelMember,
                excludeUid: item.SenderPeer.PeerId,
                pts: aggregateEvent.Pts).ConfigureAwait(false);
        }
    }

    private Task HandleReceiveMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts
        );
    }

    private async Task HandleSendMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel)
        {
            await HandleSendMessageToChannelAsync(aggregateEvent);
            return;
        }

        var selfUpdates = _updatesConverter.ToSelfUpdates(aggregateEvent);
        var ptsType = PtsType.OtherUpdates;
        IMessage? newMessage = null;
        if (item.MessageSubType == MessageSubType.Normal ||
            item.MessageSubType == MessageSubType.ForwardMessage)
        {
            ptsType = PtsType.NewMessages;
            newMessage = _messageConverter.ToMessage(item);
        }

        // when reqMsgId==0?
        // forward message reqMsgId==0
        if (aggregateEvent.RequestInfo.ReqMsgId == 0)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                ptsType: ptsType
            ).ConfigureAwait(false);
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo.ReqMsgId,
                item.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                item.SenderPeer.PeerId,
                aggregateEvent.Pts
            ).ConfigureAwait(false);
        }

        await PushUpdatesToPeerAsync(item.SenderPeer,
            _updatesConverter.ToSelfOtherDeviceUpdates(aggregateEvent),
            aggregateEvent.RequestInfo.AuthKeyId,
            pts: aggregateEvent.Pts,
            newMessage: newMessage
        ).ConfigureAwait(false);
    }

    private async Task HandleSendMessageToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var selfUpdates = _updatesConverter.ToSelfUpdates(aggregateEvent);
        var channelUpdates = _updatesConverter.ToChannelMessageUpdates(aggregateEvent);
        var ptsType = PtsType.OtherUpdates;
        if (item.MessageSubType == MessageSubType.Normal || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            ptsType = PtsType.NewMessages;
        }

        var globalSeqNo = await SavePushUpdatesAsync(item.ToPeer,
            channelUpdates.ToBytes(),
            aggregateEvent.Pts,
            ptsType,
            aggregateEvent.RequestInfo.AuthKeyId,
            aggregateEvent.RequestInfo.UserId,
            0).ConfigureAwait(false);
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(aggregateEvent.RequestInfo.ReqMsgId, item.SenderPeer.PeerId, globalSeqNo).ConfigureAwait(false);

        if (aggregateEvent.RequestInfo.ReqMsgId == 0 || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                ptsType: ptsType
            ).ConfigureAwait(false);
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo.ReqMsgId,
                aggregateEvent.MessageItem.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                aggregateEvent.RequestInfo.UserId,
                aggregateEvent.Pts
            ).ConfigureAwait(false);

            var newMessage = _messageConverter.ToMessage(item);
            await PushUpdatesToPeerAsync(item.SenderPeer,
                _updatesConverter.ToSelfOtherDeviceUpdates(aggregateEvent),
                excludeAuthKeyId: aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                newMessage: newMessage
            ).ConfigureAwait(false);
        }
        await PushUpdatesToPeerAsync(item.ToPeer, channelUpdates, aggregateEvent.RequestInfo.AuthKeyId).ConfigureAwait(false);
    }

    private Task HandleUpdatePinnedMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToUpdatePinnedMessageUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer, updates, pts: aggregateEvent.Pts);
    }

    private async Task HandleUpdatePinnedMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        var updates = _updatesConverter.ToUpdatePinnedMessageUpdates(aggregateEvent);
        await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo.ReqMsgId,
            updates,
            sourceId,
            aggregateEvent.MessageItem.SenderPeer.PeerId,
            aggregateEvent.Pts,
            aggregateEvent.MessageItem.ToPeer.PeerType
        ).ConfigureAwait(false);
        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer, updates,
            excludeAuthKeyId: aggregateEvent.RequestInfo.AuthKeyId,
            pts: aggregateEvent.Pts);

        if (aggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            var channelUpdates = _updatesConverter.ToUpdatePinnedMessageServiceUpdates(aggregateEvent);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer,
                channelUpdates,
                aggregateEvent.RequestInfo.AuthKeyId, pts: aggregateEvent.Pts).ConfigureAwait(false);
        }
    }
}