namespace MyTelegram.Domain.Commands.Channel;

public class ReadChannelLatestNoneBotOutboxMessageCommand : Command<ChannelAggregate, ChannelId, IExecutionResult>,
    IHasCorrelationId
{
    public ReadChannelLatestNoneBotOutboxMessageCommand(ChannelId aggregateId,
        string sourceCommandId,
        Guid correlationId) : base(aggregateId)
    {
        SourceCommandId = sourceCommandId;
        CorrelationId = correlationId;
    }

    public string SourceCommandId { get; }
    public Guid CorrelationId { get; }
}
