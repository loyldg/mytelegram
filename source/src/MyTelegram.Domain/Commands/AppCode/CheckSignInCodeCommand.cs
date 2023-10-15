namespace MyTelegram.Domain.Commands.AppCode;

public class CheckSignInCodeCommand : RequestCommand2<AppCodeAggregate, AppCodeId, IExecutionResult>
{
    public CheckSignInCodeCommand(AppCodeId aggregateId,
        RequestInfo requestInfo,
        //string phoneNumber,
        //string phoneCodeHash,
        string code,
        long userId) : base(aggregateId, requestInfo)
    {
        Code = code;
        //PhoneCodeHash = phoneCodeHash;
        UserId = userId;
    }

    public string Code { get; }
    //public string PhoneCodeHash { get; }
    public long UserId { get; }
}
