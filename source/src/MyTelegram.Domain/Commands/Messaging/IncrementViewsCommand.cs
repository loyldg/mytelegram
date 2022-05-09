namespace MyTelegram.Domain.Commands.Messaging;

public class IncrementViewsCommand : Command<MessageAggregate, MessageId, IExecutionResult>
{
    public IncrementViewsCommand(MessageId aggregateId) : base(aggregateId)
    {
    }
}