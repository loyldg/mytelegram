namespace MyTelegram.Domain.Commands.Messaging;

public class StartReplyToMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public StartReplyToMessageCommand(MessageId aggregateId,
        Peer replierPeer,
        int replyToMsgId,
        Guid correlationId) : base(aggregateId)
    {
        ReplierPeer = replierPeer;
        ReplyToMsgId = replyToMsgId;
        CorrelationId = correlationId;
    }

    public Peer ReplierPeer { get; }
    public int ReplyToMsgId { get; }
    public Guid CorrelationId { get; }
}