namespace MyTelegram.Domain.Commands.Messaging;

public class StartReplyToMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public StartReplyToMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        Peer replierPeer,
        //int replyToMsgId
        IInputReplyTo replyTo
        ) : base(aggregateId, requestInfo)
    {
        ReplierPeer = replierPeer;
        ReplyTo = replyTo;
    }

    public Peer ReplierPeer { get; }
    public IInputReplyTo ReplyTo { get; }
}