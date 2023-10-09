namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteMessageCommand(MessageId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}