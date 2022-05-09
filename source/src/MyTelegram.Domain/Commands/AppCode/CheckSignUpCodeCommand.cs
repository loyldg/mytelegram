namespace MyTelegram.Domain.Commands.AppCode;

public class CheckSignUpCodeCommand : RequestCommand2<AppCodeAggregate, AppCodeId, IExecutionResult>, IHasCorrelationId
{
    public CheckSignUpCodeCommand(AppCodeId aggregateId,
        RequestInfo request,
        //string phoneNumber,
        string phoneCodeHash,
        //string code, 
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        Guid correlationId) : base(aggregateId, request)
    {
        //PhoneNumber = phoneNumber;
        PhoneCodeHash = phoneCodeHash;
        //Code = code; 
        UserId = userId;
        AccessHash = accessHash;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        CorrelationId = correlationId;
    }

    public long AccessHash { get; }
    public string FirstName { get; }
    public string? LastName { get; }

    public string PhoneNumber { get; }

    //public string PhoneNumber { get; }
    public string PhoneCodeHash { get; }
    //public string Code { get; } 
    public long UserId { get; }

    public Guid CorrelationId { get; }
}
