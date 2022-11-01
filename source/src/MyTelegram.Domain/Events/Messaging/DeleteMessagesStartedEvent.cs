namespace MyTelegram.Domain.Events.Messaging;

public class DeleteMessagesStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public DeleteMessagesStartedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        bool isOut,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        IReadOnlyList<int> idList,
        bool revoke,
        IReadOnlyList<InboxItem> inboxItems,
        long? chatCreatorId,
        Guid correlationId) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        IsOut = isOut;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        IdList = idList;
        Revoke = revoke;
        InboxItems = inboxItems;
        ChatCreatorId = chatCreatorId;
        CorrelationId = correlationId;
    }

    public IReadOnlyList<int> IdList { get; }
    public IReadOnlyList<InboxItem> InboxItems { get; }
    public long? ChatCreatorId { get; }
    public bool IsOut { get; }
    public long OwnerPeerId { get; }
    public bool Revoke { get; }
    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public long SenderPeerId { get; }
    public Guid CorrelationId { get; }
}