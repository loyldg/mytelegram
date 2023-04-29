namespace MyTelegram.Domain.Events.User;

public class CheckUserStateCompletedEvent : AggregateEvent<UserAggregate, UserId>, IHasCorrelationId
{
    public CheckUserStateCompletedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}
