namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteSelfMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteSelfMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        int messageId) : base(aggregateId, requestInfo)
    {
        MessageId = messageId;
    }

    public int MessageId { get; }
}