namespace MyTelegram.Domain.Commands.Pts;

public class IncrementQtsCommand : RequestCommand2<PtsAggregate, PtsId, IExecutionResult>
{
    public IncrementQtsCommand(PtsId aggregateId,
        RequestInfo requestInfo,
        string encryptedMessageBoxId
    ) : base(aggregateId, requestInfo)
    {
        EncryptedMessageBoxId = encryptedMessageBoxId;
    }

    public string EncryptedMessageBoxId { get; }
}
