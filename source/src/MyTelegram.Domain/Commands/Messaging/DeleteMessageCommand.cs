namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public DeleteMessageCommand(MessageId aggregateId,
        //long reqMsgId,

        //bool revoke,
        Guid correlationId) : base(aggregateId)
    {
        //Revoke = revoke;
        CorrelationId = correlationId;
    }

    //public bool Revoke { get; }
    public Guid CorrelationId { get; }
}