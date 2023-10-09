namespace MyTelegram.Domain.Commands.Messaging;

public class StartReplyToMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public StartReplyToMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        Peer replierPeer,
        int replyToMsgId) : base(aggregateId, requestInfo)
    {
        ReplierPeer = replierPeer;
        ReplyToMsgId = replyToMsgId;
    }

    public Peer ReplierPeer { get; }
    public int ReplyToMsgId { get; }
}