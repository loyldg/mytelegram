namespace MyTelegram.Domain.Sagas.States;

public class UpdatePinnedMessageState :
    AggregateState<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageState>,
    IApply<UpdatePinnedMessageSagaStartedEvent>,
    IApply<UpdateOutboxPinnedCompletedEvent>,
    IApply<UpdateInboxPinnedCompletedEvent>,
    IApply<UpdatePinnedCompletedEvent>,
    IApply<UpdatePinnedBoxPtsCompletedEvent>,
    IApply<UpdatePinnedMessageCompletedEvent>
{
    public Dictionary<long, PinnedMsgItem> UpdatePinItems = new();
    public RequestInfo Request { get; private set; } = default!;

    public Guid CorrelationId { get; private set; }
    public int Date { get; private set; }
    public int InboxCount { get; private set; }
    public bool IsCompleted => InboxCount == UpdatedInboxCount;
    public string? MessageActionData { get; private set; }

    public bool NeedWaitForOutboxPinnedUpdated { get; private set; }
    public bool Pinned { get; private set; }

    public int PinnedMsgId { get; private set; }
    public bool PmOneSide { get; private set; }
    public long RandomId { get; private set; }
    // public bool ReceiveOutboxPinnedUpdated { get; private set; }
    public int ReplyToMsgId { get; private set; }

    public int SenderMessageId { get; private set; }
    public long SenderPeerId { get; private set; }
    public bool Silent { get; private set; }
    public long StartUpdatePinnedOwnerPeerId { get; private set; }
    public Peer ToPeer { get; private set; } = default!;
    public int UpdatedInboxCount { get; private set; }

    public void Apply(UpdateInboxPinnedCompletedEvent aggregateEvent)
    {
        UpdatedInboxCount++;
        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
    }

    public void Apply(UpdateOutboxPinnedCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
    }

    public void Apply(UpdatePinnedMessageCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(UpdatePinnedBoxPtsCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
        if (ToPeer.PeerType == PeerType.Channel)
        {
            UpdatedInboxCount++;
        }
    }

    public void Apply(UpdatePinnedCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }

    public void Apply(UpdatePinnedMessageSagaStartedEvent aggregateEvent)
    {
        Request = aggregateEvent.Request;
        ToPeer = aggregateEvent.ToPeer;
        StartUpdatePinnedOwnerPeerId = aggregateEvent.OwnerPeerId;
        NeedWaitForOutboxPinnedUpdated = aggregateEvent.NeedWaitForOutboxPinnedUpdated;
        InboxCount = aggregateEvent.InboxCount;
        RandomId = aggregateEvent.RandomId;
        MessageActionData = aggregateEvent.MessageActionData;
        CorrelationId = aggregateEvent.CorrelationId;
        ReplyToMsgId = aggregateEvent.ReplyToMsgId;
        SenderPeerId = aggregateEvent.SenderPeerId;
        SenderMessageId = aggregateEvent.SenderMessageId;
        Pinned = aggregateEvent.Pinned;
        PmOneSide = aggregateEvent.PmOneSide;
        Silent = aggregateEvent.Silent;
        Date = aggregateEvent.Date;
        PinnedMsgId = aggregateEvent.MessageId;
        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
    }

    public PinnedMsgItem? GetUpdatePinItem(long ownerPeerId)
    {
        if (UpdatePinItems.TryGetValue(ownerPeerId, out var item))
        {
            return item;
        }

        return default;
    }
}
