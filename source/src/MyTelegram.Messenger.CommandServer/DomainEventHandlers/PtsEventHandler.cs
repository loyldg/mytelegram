using EventFlow.Aggregates.ExecutionResults;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class PtsEventHandler(IPtsHelper ptsHelper,
    IPeerHelper peerHelper,
    IIdGenerator idGenerator,
    IQueuedCommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor) :
    //ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent>,
    ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent>,
    ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
        DeleteChannelMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
        PinChannelMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageReplyUpdatedEvent>,
    ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent>,
      ISubscribeSynchronousTo<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent>,
    ISubscribeSynchronousTo<PtsAggregate, PtsId, PtsAckedEvent>,
    ISubscribeSynchronousTo<PtsAggregate, PtsId, QtsAckedEvent>,
    ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedSagaEvent>
{
    public Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount
        );
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.NewMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.NewMessageItem.Pts
        );
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.NewMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.NewMessageItem.Pts);
    }

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
    }

    //public Task HandleAsync(
    //    IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    return UpdatePtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
    //}

    public Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId, PinChannelMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdatePtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            await IncrementGlobalSeqNoAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
        }

        foreach (var item in domainEvent.AggregateEvent.MessageItems)
        {
            //await ptsHelper.IncrementPtsAsync(item.OwnerPeer.PeerId, item.Pts);
            await UpdatePtsAsync(item.OwnerPeer.PeerId, item.Pts, messageId: item.MessageId);
        }
    }

    public async Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        foreach (var item in domainEvent.AggregateEvent.MessageItems)
        {
            //await ptsHelper.IncrementPtsAsync(item.OwnerPeer.PeerId, item.Pts);
            await UpdatePtsAsync(item.OwnerPeer.PeerId, item.Pts, messageId: item.MessageId);
        }
    }

    private async Task IncrementGlobalSeqNoAsync(long userId)
    {
        var globalSeqNo = await idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);
        var command = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
        ptsCommandExecutor.Enqueue(command);
    }

    private async Task UpdatePtsAsync(long ownerPeerId, int newPts, int ptsCount = 1, long permAuthKeyId = 0,
        int changedUnreadCount = 0, int? messageId = null)
    {
        await ptsHelper.IncrementPtsAsync(ownerPeerId, newPts, ptsCount, permAuthKeyId, changedUnreadCount);

        var command = new UpdatePtsCommand(PtsId.Create(ownerPeerId),
            ownerPeerId,
            permAuthKeyId,
            newPts,
            0,
            changedUnreadCount,
            messageId
        );
        ptsCommandExecutor.Enqueue(command);
    }

    public async Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, PtsAckedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (!peerHelper.IsChannelPeer(domainEvent.AggregateEvent.PeerId))
        {
            if (await ptsHelper.UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId,
                    domainEvent.AggregateEvent.PermAuthKeyId,
                    domainEvent.AggregateEvent.Pts,
                    domainEvent.AggregateEvent.IsFromGetDifference
                ))
            {
                await UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
                    domainEvent.AggregateEvent.Pts, 0, domainEvent.AggregateEvent.GlobalSeqNo);
                return;
            }
        }

        await UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
            domainEvent.AggregateEvent.Pts, 0, domainEvent.AggregateEvent.GlobalSeqNo);
    }

    public Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, QtsAckedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdateQtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
            domainEvent.AggregateEvent.Qts, domainEvent.AggregateEvent.GlobalSeqNo);
    }

    private Task UpdateQtsForAuthKeyIdAsync(long ownerPeerId, long permAuthKeyId, int qts, long globalSeqNo)
    {
        var command =
            new UpdateQtsForAuthKeyIdCommand(PtsId.Create(ownerPeerId, permAuthKeyId), ownerPeerId, permAuthKeyId,
                qts,
                globalSeqNo);

        ptsCommandExecutor.Enqueue(command);

        return Task.CompletedTask;
    }

    private Task UpdatePtsForAuthKeyIdAsync(long ownerPeerId, long permAuthKeyId, int pts, int changedUnreadCount, long globalSeqNo)
    {
        var updatePtsForAuthKeyIdCommand =
            new UpdatePtsForAuthKeyIdCommand(PtsId.Create(ownerPeerId, permAuthKeyId), ownerPeerId, permAuthKeyId,
                pts,
                changedUnreadCount,
                globalSeqNo);

        ptsCommandExecutor.Enqueue(updatePtsForAuthKeyIdCommand);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        return IncrementGlobalSeqNoAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
    }
}