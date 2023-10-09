namespace MyTelegram.Domain.Commands.Channel;

public class StartDeleteParticipantHistoryCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//,
    //IHasCorrelationId
{
    public StartDeleteParticipantHistoryCommand(ChannelId aggregateId,
        RequestInfo requestInfo, List<int> messageIds) : base(aggregateId, requestInfo)
    {
        MessageIds = messageIds;
    }

    public List<int> MessageIds { get; }
}