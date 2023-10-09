namespace MyTelegram.Domain.Events.User;

public class CheckUserStatusCompletedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    public CheckUserStatusCompletedEvent(
        RequestInfo requestInfo,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        //string userName,
        bool hasPassword,
        bool isUserLocked) : base(requestInfo)
    {
        UserId = userId;
        AccessHash = accessHash;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        HasPassword = hasPassword;
        IsUserLocked = isUserLocked;

    }


    public string FirstName { get; }
    public bool HasPassword { get; }
    public bool IsUserLocked { get; }
    public string? LastName { get; }
    public string PhoneNumber { get; }

    public long UserId { get; }
    public long AccessHash { get; }
}