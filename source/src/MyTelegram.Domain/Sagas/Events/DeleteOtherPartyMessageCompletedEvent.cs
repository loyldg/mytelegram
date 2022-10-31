namespace MyTelegram.Domain.Sagas.Events;

public class DeleteOtherPartyMessageCompletedEvent : AggregateEvent<DeleteMessageSaga2, DeleteMessageSaga2Id>
{
    public DeleteOtherPartyMessageCompletedEvent(bool isOut, int otherPartyMessagesCount, long ownerPeerId, int messageId)
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