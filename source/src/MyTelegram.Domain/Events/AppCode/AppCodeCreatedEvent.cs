namespace MyTelegram.Domain.Events.AppCode;

public class AppCodeCreatedEvent : RequestAggregateEvent2<AppCodeAggregate, AppCodeId>
{
    public AppCodeCreatedEvent(RequestInfo requestInfo,
        long userId,
        string phoneNumber,
        string code,
        int expire,
        string phoneCodeHash,
        long creationTime) : base(requestInfo)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        Code = code;
        Expire = expire;
        PhoneCodeHash = phoneCodeHash;
        CreationTime = creationTime;
    }

    public string Code { get; }
    public long CreationTime { get; }
    public int Expire { get; }
    public string PhoneCodeHash { get; }
    public string PhoneNumber { get; }
    public long UserId { get; }
}
