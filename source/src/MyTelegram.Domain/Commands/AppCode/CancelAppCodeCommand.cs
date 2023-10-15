namespace MyTelegram.Domain.Commands.AppCode;

public class CancelAppCodeCommand : RequestCommand2<AppCodeAggregate, AppCodeId, IExecutionResult>
{
    public CancelAppCodeCommand(AppCodeId aggregateId,
        RequestInfo requestInfo,
        string phoneNumber,
        string phoneCodeHash) : base(aggregateId, requestInfo)
    {
        PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
    }

    public string PhoneCodeHash { get; }
    public string PhoneNumber { get; }
}