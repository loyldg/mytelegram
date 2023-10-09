namespace MyTelegram.Domain.Sagas.States;

public class DeleteMessageSaga2State : AggregateState<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessageSaga2State>,
    IApply<DeleteMessageSaga2StartedEvent>,
    IApply<DeleteSelfMessageCompletedEvent>,
    IApply<DeleteOtherPartyMessageCompletedEvent>,
    IApply<DeleteMessagePtsIncrementedEvent2>
{
    private readonly Dictionary<long, List<int>> _peerToMessages = new();
    private readonly Dictionary<long, int> _peerToPts = new();
    public long ChatCreatorUserId { get; private set; }
    public int ChatMemberCount { get; private set; }
    public Guid CorrelationId { get; private set; }
    public int DeletedOtherPartyMessagesCount { get; private set; }
    public int DeletedSelfMessagesCount { get; private set; }
    public int TotalOtherPartyOutboxMessageCount { get; private set; }
    public int DeletedOtherPartyOutboxMessageCount { get; private set; }
    public RequestInfo RequestInfo { get; private set; } = null!;
    public bool Revoke { get; private set; }
    //public int TotalCountToDelete { get; private set; }
    //public int DeletedCount { get; private set; }

    public int TotalOtherPartyMessagesCount { get; private set; }
    public int TotalSelfMessagesCount { get; private set; }

    public PeerType ToPeerType { get; private set; }
    public bool IsClearHistory { get; private set; }
    public void Apply(DeleteMessageSaga2StartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        Revoke = aggregateEvent.Revoke;
        ChatCreatorUserId = aggregateEvent.ChatCreatorUserId;
        ChatMemberCount = aggregateEvent.ChatMemberCount;
        IsClearHistory = aggregateEvent.IsClearHistory;
        TotalSelfMessagesCount = aggregateEvent.IdList.Count;
        ToPeerType = aggregateEvent.ToPeerType;
    }

    public void Apply(DeleteSelfMessageCompletedEvent aggregateEvent)
    {
        DeletedSelfMessagesCount++;
        TotalOtherPartyMessagesCount += aggregateEvent.OtherPartyMessagesCount;
        AddMessageIdToDeleteList(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId);

        if (!aggregateEvent.IsOut)
        {
            TotalOtherPartyOutboxMessageCount++;
        }
    }

    public void Apply(DeleteOtherPartyMessageCompletedEvent aggregateEvent)
    {
        DeletedOtherPartyMessagesCount++;
        TotalOtherPartyMessagesCount += aggregateEvent.OtherPartyMessagesCount;
        AddMessageIdToDeleteList(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId);
        if (aggregateEvent.IsOut)
        {
            DeletedOtherPartyOutboxMessageCount++;
        }
    }

    public void Apply(DeleteMessagePtsIncrementedEvent2 aggregateEvent)
    {
        UpdatePeerPts(aggregateEvent.PeerId, aggregateEvent.Pts);
    }

    public IReadOnlyList<DeletedBoxItem> GetDeletedBoxes()
    {
        var boxList = new List<DeletedBoxItem>();
        foreach (var item in _peerToMessages)
        {
            if (_peerToPts.TryGetValue(item.Key, out var pts))
            {
                boxList.Add(new DeletedBoxItem(item.Key, pts, item.Value.Count, item.Value));
            }
        }

        return boxList;
    }

    public DeletedBoxItem GetDeletedBoxItem(long ownerPeerId)
    {
        _peerToMessages.TryGetValue(ownerPeerId, out var messageIdList);
        _peerToPts.TryGetValue(ownerPeerId, out var pts);

        return new DeletedBoxItem(ownerPeerId, pts, messageIdList?.Count ?? 0, messageIdList ?? new List<int>());
    }

    public bool IsDeleteMessagesCompleted()
    {
        var selfMessageDeletedSuccess = TotalSelfMessagesCount == DeletedSelfMessagesCount;
        if (!Revoke)
        {
            return selfMessageDeletedSuccess;
        }

        var otherPartyMessagesDeletedSuccess = TotalOtherPartyMessagesCount == DeletedOtherPartyMessagesCount;
        var otherPartyOutboxMessageDeleted = TotalOtherPartyOutboxMessageCount == DeletedOtherPartyOutboxMessageCount;

        return selfMessageDeletedSuccess && otherPartyMessagesDeletedSuccess && otherPartyOutboxMessageDeleted;
    }
    private void AddMessageIdToDeleteList(long peerId,
        int messageId)
    {
        if (!_peerToMessages.TryGetValue(peerId, out var messageIds))
        {
            messageIds = new();
            _peerToMessages.TryAdd(peerId, messageIds);
        }
        messageIds.Add(messageId);
    }

    private void UpdatePeerPts(long peerId,
        int pts)
    {
        if (_peerToPts.ContainsKey(peerId))
        {
            _peerToPts[peerId] = pts;
        }
        else
        {
            _peerToPts.TryAdd(peerId, pts);
        }
    }
}