namespace MyTelegram.Domain.Commands.AppCode;

public class CheckSignInCodeCommand : RequestCommand2<AppCodeAggregate, AppCodeId, IExecutionResult>,
    IHasCorrelationId
{
    public CheckSignInCodeCommand(AppCodeId aggregateId,
        RequestInfo request,
        //string phoneNumber,
        string phoneCodeHash,
        //string code,
        long userId,
        Guid correlationId) : base(aggregateId, request)
    {
        //Code = code;
        PhoneCodeHash = phoneCodeHash;
        UserId = userId;
        CorrelationId = correlationId;
    }

    //public string Code { get; }
    public string PhoneCodeHash { get; }
    public long UserId { get; }

    public Guid CorrelationId { get; }
}
