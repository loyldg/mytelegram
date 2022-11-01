namespace MyTelegram.Domain.Commands.Dialog;

public class ClearHistoryCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>, IHasCorrelationId
{
    //public bool Revoke { get; }

    public ClearHistoryCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        bool revoke,
        string messageActionData,
        long randomId,
        List<int> messageIdListToBeDelete,
        int nextMaxId,
        Guid correlationId) : base(aggregateId, requestInfo)
    {
        //Revoke = revoke; 
        Revoke = revoke;
        MessageActionData = messageActionData;
        RandomId = randomId;
        MessageIdListToBeDelete = messageIdListToBeDelete;
        NextMaxId = nextMaxId;
        CorrelationId = correlationId;
    }

    public string MessageActionData { get; }
    public List<int> MessageIdListToBeDelete { get; }
    public int NextMaxId { get; }
    public long RandomId { get; }
    public bool Revoke { get; } 
    public Guid CorrelationId { get; }
}
