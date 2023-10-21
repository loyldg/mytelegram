namespace MyTelegram.Domain.Sagas.Events;

public class EditInboxMessageStartedEvent : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public EditInboxMessageStartedEvent(long userId,
        int messageId)
    {
        UserId = userId;
        MessageId = messageId;
    }

    public int MessageId { get; }

    public long UserId { get; }
}
