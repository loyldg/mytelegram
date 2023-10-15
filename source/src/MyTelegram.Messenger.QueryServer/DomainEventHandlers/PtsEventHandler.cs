using EventFlow.Aggregates.ExecutionResults;
using MyTelegram.Messenger.QueryServer.Services;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class PtsEventHandler :
    ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent>,
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent2>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent2>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
    ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<PtsAggregate, PtsId, PtsAckedEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;
    private readonly IPeerHelper _peerHelper;
    private readonly IIdGenerator _idGenerator;
    private readonly ICommandExecutor<PtsAggregate, PtsId, IExecutionResult> _ptsCommandExecutor;

    public PtsEventHandler(IPtsHelper ptsHelper, ICommandBus commandBus, IPeerHelper peerHelper, IIdGenerator idGenerator, ICommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor)
    {
        _ptsHelper = ptsHelper;
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _idGenerator = idGenerator;
        _ptsCommandExecutor = ptsCommandExecutor;
    }

    public async Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts, domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagePtsIncrementedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, changedUnreadCount: 1);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, newUnreadCount: 1);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, PtsAckedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
            domainEvent.AggregateEvent.Pts, 0, domainEvent.AggregateEvent.GlobalSeqNo);

        //var command = new PtsAckedCommand(
        //    PtsId.Create(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId),
        //    domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
        //    domainEvent.AggregateEvent.MsgId, domainEvent.AggregateEvent.Pts, domainEvent.AggregateEvent.GlobalSeqNo,
        //    domainEvent.AggregateEvent.ToPeer);
        //await _commandBus.PublishAsync(command, default);
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts,
            newUnreadCount: -domainEvent.AggregateEvent.ReadCount);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId, -domainEvent.AggregateEvent.ReadCount);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent2> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, newUnreadCount: 1);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, changedUnreadCount: 1);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent2> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);

        if (domainEvent.AggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            await IncrementGlobalSeqNoAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
            //await UpdateChannelPtsForUserAsync(domainEvent.AggregateEvent.RequestInfo.UserId,
            //    domainEvent.AggregateEvent.MessageItem.ToPeer.PeerId, domainEvent.AggregateEvent.Pts,
            //    domainEvent.AggregateEvent.GlobalSeqNo);
        }
        else
        {
            await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
        }
        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);

        //return Task.CompletedTask;
    }

    private async Task IncrementTempPtsAsync(long ownerPeerId, int newPts, long permAuthKeyId = 0,
        int changedUnreadCount = 0)
    {
        //var command = new IncrementTempPtsCommand(TempPtsId.New, ownerPeerId, newPts, permAuthKeyId);

        Task.Run(() =>
        {
            var command =
                new UpdatePtsCommand(
                    PtsId.Create(ownerPeerId),
                    ownerPeerId,
                    permAuthKeyId,
                    newPts,
                    0,
                    changedUnreadCount);

            _ptsCommandExecutor.Enqueue(command);

            //_commandBus.PublishAsync(command, default);
        });

        //if (permAuthKeyId != 0)
        //{
        //    await UpdatePtsForAuthKeyIdAsync(ownerPeerId, permAuthKeyId, newPts, changedUnreadCount, 0);
        //}
    }

    private Task UpdatePtsForAuthKeyIdAsync(long ownerPeerId, long permAuthKeyId, int pts, int changedUnreadCount, long globalSeqNo)
    {
        Task.Run(() =>
        {
            //Console.WriteLine(
            //    $"update pts for auth key id:{ownerPeerId} {permAuthKeyId} {pts} {globalSeqNo} changedUnreadCount:{changedUnreadCount}");
            var updatePtsForAuthKeyIdCommand =
                new UpdatePtsForAuthKeyIdCommand(PtsId.Create(ownerPeerId, permAuthKeyId), ownerPeerId, permAuthKeyId,
                    pts,
                    changedUnreadCount,
                    globalSeqNo);

            _ptsCommandExecutor.Enqueue(updatePtsForAuthKeyIdCommand);

            //return _commandBus.PublishAsync(updatePtsForAuthKeyIdCommand, default);
        });

        return Task.CompletedTask;
    }

    private Task IncrementGlobalSeqNoAsync(long userId)
    {
        Task.Run(async() =>
        {
            var globalSeqNo = await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);
            var command = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
            _ptsCommandExecutor.Enqueue(command);
            //await _commandBus.PublishAsync(command, default);
        });

        return Task.CompletedTask;
    }

    //private Task UpdateChannelPtsForUserAsync(long userId, long channelId, int pts, long globalSeqNo)
    //{
    //    var command = new UpdateChannelPtsForUserCommand(PtsId.Create(userId), userId, channelId, pts, globalSeqNo);
    //    return _commandBus.PublishAsync(command, default);
    //}
}