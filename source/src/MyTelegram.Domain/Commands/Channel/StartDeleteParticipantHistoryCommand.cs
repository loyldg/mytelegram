namespace MyTelegram.Domain.Commands.Channel;

public class StartDeleteParticipantHistoryCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>,
    IHasCorrelationId
{
    public StartDeleteParticipantHistoryCommand(ChannelId aggregateId,
        RequestInfo requestInfo,List<int> messageIds,Guid correlationId) : base(aggregateId, requestInfo)
    {
        MessageIds = messageIds;
        CorrelationId = correlationId;
    }

    public List<int> MessageIds { get; }
    public Guid CorrelationId { get; }
}