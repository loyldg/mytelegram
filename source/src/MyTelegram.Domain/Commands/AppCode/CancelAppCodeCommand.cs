namespace MyTelegram.Domain.Commands.AppCode;

public class CancelAppCodeCommand : RequestCommand<AppCodeAggregate, AppCodeId, IExecutionResult>
{
    public CancelAppCodeCommand(AppCodeId aggregateId,
        long reqMsgId,
        string phoneNumber,
        string phoneCodeHash) : base(aggregateId, reqMsgId)
    {
        PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
    }

    public string PhoneCodeHash { get; }
    public string PhoneNumber { get; }
}
