namespace MyTelegram.Domain.Sagas.Events;

public class DeleteSelfMessageCompletedEvent : AggregateEvent<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    public DeleteSelfMessageCompletedEvent(bool isOut,
        int otherPartyMessagesCount,
        long ownerPeerId,
        int messageId)
    {
        IsOut = isOut;
        OtherPartyMessagesCount = otherPartyMessagesCount;
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
    }

    public int MessageId { get; }
    public bool IsOut { get; }
    public int OtherPartyMessagesCount { get; }
    public long OwnerPeerId { get; }
}
