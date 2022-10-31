using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class OtherDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignInSuccessEvent>,
    ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignUpRequiredEvent>,
    ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent>,
    ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent>,
    ISubscribeSynchronousTo<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessagesCompletedEvent2>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
    ISubscribeSynchronousTo<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId, DeleteParticipantHistoryCompletedEvent>
{
    private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly IEventBus _eventBus;
    private readonly ILogger<OtherDomainEventHandler> _logger;
    private readonly ITlUpdatesConverter _updatesConverter;

    public OtherDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        ITlUpdatesConverter updatesConverter,
        ITlAuthorizationConverter authorizationConverter,
        IEventBus eventBus,
        ILogger<OtherDomainEventHandler> logger) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _updatesConverter = updatesConverter;
        _authorizationConverter = authorizationConverter;
        _eventBus = eventBus;
        _logger = logger;
    }

    public Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignUpRequiredEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
            _authorizationConverter.CreateSignUpAuthorization());
    }

    public async Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.IsSelf)
        {
            var r = new TAffectedHistory
            {
                Pts = domainEvent.AggregateEvent.DeletedBoxItem.Pts,
                PtsCount = domainEvent.AggregateEvent.DeletedBoxItem.PtsCount,
                Offset = domainEvent.AggregateEvent.NextMaxId
            };
            //if (domainEvent.AggregateEvent.ToPeerType != PeerType.Channel)
            //{
            //    //await AddRpcPtsForAuthKeyIdAsync(domainEvent.AggregateEvent.ReqMsgId,domainEvent.AggregateEvent)
            //}
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
                r,
                domainEvent.Metadata.SourceId.Value
            ).ConfigureAwait(false);
            // Console.WriteLine($"Clear history completed for {domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId} reqMsgId:{domainEvent.AggregateEvent.ReqMsgId}");
        }

        var updates = _updatesConverter.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
            domainEvent.AggregateEvent.DeletedBoxItem,
            DateTime.UtcNow.ToTimestamp());
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId),
            updates,
            pts: domainEvent.AggregateEvent.DeletedBoxItem.Pts).ConfigureAwait(false);

        //var r = new TAffectedHistory
        //{
        //    Pts = domainEvent.AggregateEvent.Pts,
        //    PtsCount = domainEvent.AggregateEvent.PtsCount,
        //    Offset = 0
        //};
        //if (domainEvent.AggregateEvent.ReqMsgId != 0)
        //{
        //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, () => r);
        //}
        ////else
        ////{
        ////    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId), r);
        ////}
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var date = DateTime.UtcNow.ToTimestamp();
        IUpdates? channelUpdates = null;
        Peer channelPeer;
        if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
        {
            channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
            channelUpdates = _updatesConverter.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
                domainEvent.AggregateEvent.SelfDeletedBoxItem,
                date);
            var globalSeqNo = await SavePushUpdatesAsync(
                channelPeer,
                channelUpdates.ToBytes(),
                domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
                PtsType.OtherUpdates,
                domainEvent.AggregateEvent.Request.AuthKeyId,
                0,
                0
            ).ConfigureAwait(false);
            await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
                domainEvent.AggregateEvent.Request.UserId,
                globalSeqNo).ConfigureAwait(false);
            await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.Request.UserId,
                globalSeqNo).ConfigureAwait(false);
        }

        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
            PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
            r,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.Request.UserId,
            domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
            domainEvent.AggregateEvent.ToPeerType).ConfigureAwait(false);

        if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
        {
            channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
            await PushUpdatesToChannelMemberAsync(channelPeer,
                channelUpdates!,
                domainEvent.AggregateEvent.Request.AuthKeyId,
                skipSaveUpdates: true).ConfigureAwait(false);
        }
        else
        {
            foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
            {
                var excludeAuthKeyId = 0L;
                if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
                {
                    excludeAuthKeyId = domainEvent.AggregateEvent.Request.AuthKeyId;
                }

                var updates =
                    _updatesConverter.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
                        deletedBoxItem,
                        date);
                await PushUpdatesToPeerAsync(
                    new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
                    updates,
                    excludeAuthKeyId,
                    pts: deletedBoxItem.Pts
                ).ConfigureAwait(false);
            }
        }
    }

    private async Task HandleClearHistoryCompletedAsync(DeleteMessagesCompletedEvent2 aggregateEvent)
    {
        var r = new TAffectedHistory
        {
            Pts = aggregateEvent.SelfDeletedBoxItem.Pts,
            PtsCount = aggregateEvent.SelfDeletedBoxItem.PtsCount,
            Offset = aggregateEvent.SelfDeletedBoxItem.DeletedMessageIdList.Min()
        };
        await SendRpcMessageToClientAsync(aggregateEvent.Request.ReqMsgId, r).ConfigureAwait(false);
        var date = DateTime.UtcNow.ToTimestamp();
        foreach (var deletedBoxItem in aggregateEvent.DeletedBoxItems)
        {
            var updates = _updatesConverter
                .ToDeleteMessagesUpdates(aggregateEvent.ToPeerType, deletedBoxItem, date);
            await PushUpdatesToPeerAsync(new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
                updates).ConfigureAwait(false);
        }
    }
    public async Task HandleAsync(IDomainEvent<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessagesCompletedEvent2> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.IsClearHistory)
        {
            await HandleClearHistoryCompletedAsync(domainEvent.AggregateEvent).ConfigureAwait(false);
            return;
        }
        //{
        var date = DateTime.UtcNow.ToTimestamp();
        //    _logger.LogDebug("SignUpRequired userId={UserId} phoneUmber={PhoneNumber}",
        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
            PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
        };
        //        domainEvent.AggregateEvent.UserId,
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
            r,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.Request.UserId,
            domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
            domainEvent.AggregateEvent.ToPeerType).ConfigureAwait(false);
        //        _rpcResultProcessor.CreateSignUpAuthorization()).ConfigureAwait(false);
        foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
        {
            var excludeAuthKeyId = 0L;
            if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
            {
                excludeAuthKeyId = domainEvent.AggregateEvent.Request.AuthKeyId;
            }
            var updates =
                _updatesConverter
                    .ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
                        deletedBoxItem,
                        date);

            await PushUpdatesToPeerAsync(
                new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
                updates,
                excludeAuthKeyId,
                pts: deletedBoxItem.Pts
            ).ConfigureAwait(false);
        }
    }
    public async Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignInSuccessEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var tempAuthKeyId = domainEvent.AggregateEvent.TempAuthKeyId;
        //await _sessionAppService.BindUserIdToSessionAsync(tempAuthKeyId, domainEvent.AggregateEvent.UserId).ConfigureAwait(false);
        //_sessionAppService.SetSessionPasswordState(domainEvent.AggregateEvent.TempAuthKeyId, domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None);

        await _eventBus.PublishAsync(new UserSignInSuccessEvent(tempAuthKeyId,
                domainEvent.AggregateEvent.PermAuthKeyId,
                domainEvent.AggregateEvent.UserId,
                domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None))
            .ConfigureAwait(false);
        _logger.LogDebug(
            "########################### User sign in success:{UserId} with tempAuthKeyId:{TempAuthKeyId} permAuthKeyId:{PermAuthKeyId}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.TempAuthKeyId,
            domainEvent.AggregateEvent.PermAuthKeyId);

        if (domainEvent.AggregateEvent.HasPassword)
        {
            //throw new BadRequestException("SESSION_PASSWORD_NEEDED");
            //ThrowHelper.ThrowUserFriendlyException("SESSION_PASSWORD_NEEDED");
            var rpcError = new TRpcError
            {
                ErrorCode = MyTelegramServerDomainConsts.BadRequestErrorCode,
                ErrorMessage = "SESSION_PASSWORD_NEEDED"
            };
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, rpcError).ConfigureAwait(false);
            return;
        }

        var r = _authorizationConverter.CreateAuthorization(domainEvent.AggregateEvent);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = _updatesConverter.ToSelfUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
        if (domainEvent.AggregateEvent.PmOneSide || domainEvent.AggregateEvent.ShouldReplyRpcResult)
        {
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
                r,
                domainEvent.Metadata.SourceId.Value,
                domainEvent.AggregateEvent.SenderPeerId,
                domainEvent.AggregateEvent.Pts,
                //PtsType.OtherUpdates,
                domainEvent.AggregateEvent.ToPeer.PeerType
            ).ConfigureAwait(false);
            await PushUpdatesToPeerAsync(
                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
                r,
                pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
        }

        //else
        {
            await PushUpdatesToPeerAsync(
                domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
                    ? new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId)
                    : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
                _updatesConverter.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent),
                excludeUid: domainEvent.AggregateEvent.SenderPeerId,
                pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
        }
    }
    public async Task HandleAsync(IDomainEvent<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId, DeleteParticipantHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = domainEvent.AggregateEvent.NextMaxId
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId, r).ConfigureAwait(false);
        var date = DateTime.UtcNow.ToTimestamp();
        var deletedBoxItem = new DeletedBoxItem(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.PtsCount,
            domainEvent.AggregateEvent.MessageIds);
        var updates = _updatesConverter
            .ToDeleteMessagesUpdates(PeerType.Channel,
                deletedBoxItem,
                date);
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId),
            updates,
            pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
    }
}
