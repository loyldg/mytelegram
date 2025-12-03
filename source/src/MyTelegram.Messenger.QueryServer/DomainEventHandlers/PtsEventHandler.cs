using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class PtsEventHandler(
    IPtsHelper ptsHelper)
    :
        //ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId,
        //    UpdatePinnedBoxPtsCompletedSagaEvent>,
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
        ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId,
            DeleteReplyMessagePtsIncrementedSagaEvent>,
        ISubscribeSynchronousTo<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent>,
        ISubscribeSynchronousTo<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent>,
        ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent>
{
    public async Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
            DeleteChannelMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent>
            domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.NewMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.NewMessageItem.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.NewMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.NewMessageItem.Pts);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
            PinChannelMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts,
            newUnreadCount: -domainEvent.AggregateEvent.ReadCount);
    }

    public async Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        foreach (var item in domainEvent.AggregateEvent.MessageItems)
        {
            await ptsHelper.IncrementPtsAsync(item.OwnerPeer.PeerId, item.Pts);
        }
    }

    public Task HandleAsync(
        IDomainEvent<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        return Task.CompletedTask;
    }

    //public async Task HandleAsync(
    //    IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent>
    //        domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
    //}
}