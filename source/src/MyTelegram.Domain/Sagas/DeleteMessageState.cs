namespace MyTelegram.Domain.Sagas;

public class DeleteMessageState : AggregateState<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessageState>,
    IApply<DeleteMessagesStartedEvent>,
    IApply<DeleteMessagePtsIncrementedEvent>,
    IApply<DeleteSingleMessageCompletedEvent>,
    IApply<DeleteMessagesSagaStartedEvent>
{
    private readonly Dictionary<long, List<int>> _ownerPeerIdToMessageIdDict = new();
    private readonly Dictionary<long, int> _peerToPts = new();

    public RequestInfo RequestInfo { get; private set; } = default!;

    public int DeletedCount { get; private set; }
    public bool IsCompleted => TotalCount == DeletedCount;
    public bool Revoke { get; private set; }
    public int SelfDeletedCount { get; private set; }
    public int SelfNeedDeleteCount { get; private set; }
    public Peer ToPeer { get; private set; } = default!;
    public int TotalCount { get; private set; }
    public long? ChatCreatorId { get; private set; }
    public void Apply(DeleteMessagePtsIncrementedEvent aggregateEvent)
    {
        if (_peerToPts.ContainsKey(aggregateEvent.PeerId))
        {
            _peerToPts[aggregateEvent.PeerId] = aggregateEvent.Pts;
        }
        else
        {
            _peerToPts.TryAdd(aggregateEvent.PeerId, aggregateEvent.Pts);
        }
    }

    public void Apply(DeleteMessagesStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        SelfNeedDeleteCount = aggregateEvent.IdList.Count;
        Revoke = aggregateEvent.Revoke;
        ToPeer = aggregateEvent.ToPeer;
    }

    public void Apply(DeleteSingleMessageCompletedEvent aggregateEvent)
    {
        if (aggregateEvent.IsSelfMessage)
        {
            if (Revoke)
            {
                TotalCount += aggregateEvent.InboxItemCount;
            }

            SelfDeletedCount++;
        }
        else
        {
            if (aggregateEvent.OwnerPeerId != RequestInfo.UserId)
            {
                DeletedCount++;
            }
        }

        if (_ownerPeerIdToMessageIdDict.TryGetValue(aggregateEvent.OwnerPeerId, out var messageIdList))
        {
            messageIdList.Add(aggregateEvent.MessageId);
        }
        else
        {
            _ownerPeerIdToMessageIdDict.TryAdd(aggregateEvent.OwnerPeerId, new List<int> { aggregateEvent.MessageId });
        }
    }

    public IReadOnlyList<DeletedBoxItem> GetDeletedBoxes()
    {
        var boxList = new List<DeletedBoxItem>();
        foreach (var item in _ownerPeerIdToMessageIdDict)
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
        _ownerPeerIdToMessageIdDict.TryGetValue(ownerPeerId, out var messageIdList);
        _peerToPts.TryGetValue(ownerPeerId, out var pts);

        return new DeletedBoxItem(ownerPeerId, pts, messageIdList?.Count ?? 0, messageIdList ?? new List<int>());
    }

    public bool IsMessageDeletedCompleted()
    {
        if (SelfNeedDeleteCount == SelfDeletedCount)
        {
            if (TotalCount == DeletedCount)
            {
                return true;
            }
        }

        return false;
    }

    public void Apply(DeleteMessagesSagaStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        SelfNeedDeleteCount = aggregateEvent.IdList.Count;
        Revoke = aggregateEvent.Revoke;
        ToPeer = aggregateEvent.ToPeer;
        ChatCreatorId = aggregateEvent.ChatCreatorId;
    }
}
