namespace MyTelegram.Domain.Events.Channel;

public class IncrementParticipantCountEvent : AggregateEvent<ChannelAggregate, ChannelId> //, IHasCorrelationId
{
    public long ChannelId { get; }
    public int NewParticipantCount { get; }

    public IncrementParticipantCountEvent(long channelId, int newParticipantCount)
    {
        ChannelId = channelId;
        NewParticipantCount = newParticipantCount;
    }

    //public IncrementParticipantCountEvent(Guid correlationId)
    //{
    //    
    //}

    //
}