namespace MyTelegram.Domain.Events.Channel;

public class IncrementParticipantCountEvent : AggregateEvent<ChannelAggregate, ChannelId> //, IHasCorrelationId
{
    //public IncrementParticipantCountEvent(Guid correlationId)
    //{
    //    CorrelationId = correlationId;
    //}

    //public Guid CorrelationId { get; }
}
