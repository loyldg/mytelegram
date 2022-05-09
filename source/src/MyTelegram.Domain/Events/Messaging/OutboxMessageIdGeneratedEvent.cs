using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageIdGeneratedEvent : AggregateEvent<MessageSaga, MessageSagaId>
{
    public int OutboxMessageId { get; }

    public OutboxMessageIdGeneratedEvent(int outboxMessageId)
    {
        OutboxMessageId = outboxMessageId;
    }
}