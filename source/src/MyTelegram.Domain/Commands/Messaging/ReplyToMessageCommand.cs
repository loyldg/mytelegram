namespace MyTelegram.Domain.Commands.Messaging;

public class ReplyToMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public int MessageId { get; }
    public ReplyToMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        int messageId
    ) : base(aggregateId, requestInfo)
    {
        MessageId = messageId;
    }
}