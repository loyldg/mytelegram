//namespace MyTelegram.Domain.Sagas.States;

//public class UpdatePinnedMessageState :
//    AggregateState<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageState>,
//    IApply<UpdatePinnedMessageSagaStartedSagaEvent>,
//    IApply<UpdateOutboxPinnedCompletedSagaEvent>,
//    IApply<UpdateInboxPinnedCompletedSagaEvent>,
//    IApply<UpdatePinnedCompletedSagaEvent>,
//    IApply<UpdatePinnedBoxPtsCompletedSagaEvent>,
//    IApply<UpdatePinnedMessageCompletedSagaEvent>,
//    IApply<UpdateSavedMessagesPinnedCompletedSagaEvent>
//{
//    public Dictionary<long, PinnedMsgItem> UpdatePinItems = new();
//    public RequestInfo RequestInfo { get; private set; } = default!;
//    public int Date { get; private set; }
//    public int InboxCount { get; private set; }
//    public bool IsCompleted => InboxCount == UpdatedInboxCount;
//    //public string? MessageActionData { get; private set; }
//    public IMessageAction? MessageAction { get; private set; }

//    public bool NeedWaitForOutboxPinnedUpdated { get; private set; }
//    public bool Pinned { get; private set; }

//    public int PinnedMsgId { get; private set; }
//    public bool PmOneSide { get; private set; }
//    public long RandomId { get; private set; }
//    public int ReplyToMsgId { get; private set; }
//    public int SenderMessageId { get; private set; }
//    public long SenderPeerId { get; private set; }
//    public bool Silent { get; private set; }
//    public long StartUpdatePinnedOwnerPeerId { get; private set; }
//    public Peer ToPeer { get; private set; } = null!;
//    public int UpdatedInboxCount { get; private set; }
//    public bool Post { get; private set; }
//    public void Apply(UpdateInboxPinnedCompletedSagaEvent aggregateEvent)
//    {
//        UpdatedInboxCount++;
//        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
//            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
//    }

//    public void Apply(UpdateOutboxPinnedCompletedSagaEvent aggregateEvent)
//    {
//        Post = aggregateEvent.Post;
//        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
//            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
//    }

//    public void Apply(UpdatePinnedMessageCompletedSagaEvent aggregateEvent)
//    {
//    }

//    public void Apply(UpdatePinnedBoxPtsCompletedSagaEvent aggregateEvent)
//    {
//        if (ToPeer is { PeerType: PeerType.Channel })
//        {
//            UpdatedInboxCount++;
//        }
//    }

//    public void Apply(UpdatePinnedCompletedSagaEvent aggregateEvent)
//    {
//    }

//    public void Apply(UpdatePinnedMessageSagaStartedSagaEvent aggregateEvent)
//    {
//        RequestInfo = aggregateEvent.RequestInfo;
//        ToPeer = aggregateEvent.ToPeer;
//        StartUpdatePinnedOwnerPeerId = aggregateEvent.OwnerPeerId;
//        NeedWaitForOutboxPinnedUpdated = aggregateEvent.NeedWaitForOutboxPinnedUpdated;
//        InboxCount = aggregateEvent.InboxCount;
//        RandomId = aggregateEvent.RandomId;
//        //MessageActionData = aggregateEvent.MessageActionData;
//        MessageAction = aggregateEvent.MessageAction;
//        //CorrelationId = aggregateEvent.CorrelationId;
//        ReplyToMsgId = aggregateEvent.ReplyToMsgId;
//        SenderPeerId = aggregateEvent.SenderPeerId;
//        SenderMessageId = aggregateEvent.SenderMessageId;
//        Pinned = aggregateEvent.Pinned;
//        PmOneSide = aggregateEvent.PmOneSide;
//        Silent = aggregateEvent.Silent;
//        Date = aggregateEvent.Date;
//        PinnedMsgId = aggregateEvent.MessageId;
//        UpdatePinItems.TryAdd(aggregateEvent.OwnerPeerId,
//            new PinnedMsgItem(aggregateEvent.OwnerPeerId, aggregateEvent.MessageId, aggregateEvent.ToPeer.PeerId));
//    }

//    public PinnedMsgItem? GetUpdatePinItem(long ownerPeerId)
//    {
//        if (UpdatePinItems.TryGetValue(ownerPeerId, out var item))
//        {
//            return item;
//        }

//        return default;
//    }

//    public void Apply(UpdateSavedMessagesPinnedCompletedSagaEvent aggregateEvent)
//    {
//        //throw new NotImplementedException();
//    }
//}
