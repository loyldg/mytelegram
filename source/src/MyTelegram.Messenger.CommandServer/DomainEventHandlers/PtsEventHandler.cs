﻿using MyTelegram.Messenger.Services.Caching;
using SendOutboxMessageCompletedEvent = MyTelegram.Domain.Sagas.Events.SendOutboxMessageCompletedEvent;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class PtsEventHandler :
    ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
    ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>
{
    private readonly IPtsHelper _ptsHelper;

    public PtsEventHandler(IPtsHelper ptsHelper)
    {
        _ptsHelper = ptsHelper;
    }

    public Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagePtsIncrementedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    //public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //        domainEvent.AggregateEvent.Pts);
    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, Domain.Events.Messaging.SendOutboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //        domainEvent.AggregateEvent.Pts);
    //    return Task.CompletedTask;
    //}

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }
}