namespace MyTelegram.Domain.Sagas.Events;

public class DeleteSingleMessageCompletedEvent : AggregateEvent<DeleteMessageSaga, DeleteMessageSagaId>
{
    public DeleteSingleMessageCompletedEvent(long ownerPeerId,
        int messageId,
        bool isSelfMessage,
        int inboxItemCount)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        IsSelfMessage = isSelfMessage;
        InboxItemCount = inboxItemCount;
    }

    public int InboxItemCount { get; }
    public bool IsSelfMessage { get; }
    public int MessageId { get; }

    public long OwnerPeerId { get; }
}
