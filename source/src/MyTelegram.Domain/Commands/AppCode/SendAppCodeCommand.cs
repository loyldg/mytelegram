namespace MyTelegram.Domain.Commands.AppCode;

public class SendAppCodeCommand : RequestCommand2<AppCodeAggregate, AppCodeId, IExecutionResult>
{
    public SendAppCodeCommand(AppCodeId aggregateId,
        RequestInfo requestInfo,
        long userId,
        string phoneNumber,
        string code,
        string phoneCodeHash,
        int expire,
        long creationTime) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        Code = code;
        PhoneCodeHash = phoneCodeHash;
        Expire = expire;
        CreationTime = creationTime;
    }

    public string Code { get; }
    public long CreationTime { get; }
    public int Expire { get; }

    public string PhoneCodeHash { get; }
    public long UserId { get; }
    public string PhoneNumber { get; }
}
