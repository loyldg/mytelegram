namespace MyTelegram.Domain.Commands.Messaging;

public class StartReplyToMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public int ReplyToMsgId { get; }

    public StartReplyToMessageCommand(MessageId aggregateId, int replyToMsgId, Guid correlationId) : base(aggregateId)
    {
        ReplyToMsgId = replyToMsgId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
}