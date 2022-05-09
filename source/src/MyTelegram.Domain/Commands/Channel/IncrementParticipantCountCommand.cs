namespace MyTelegram.Domain.Commands.Channel;

public class
    IncrementParticipantCountCommand : Command<ChannelAggregate, ChannelId, IExecutionResult> //, IHasCorrelationId
{
    public IncrementParticipantCountCommand(ChannelId aggregateId) : base(aggregateId)
    {
    }

/*
        public IncrementParticipantCountCommand(ChannelId aggregateId,
            ISourceId sourceId) : base(aggregateId, sourceId)
        {
        }
*/
}
