namespace MyTelegram.Domain.Events.User;

public class CheckUserStatusCompletedEvent : AggregateEvent<UserAggregate, UserId>, IHasCorrelationId
{
    public CheckUserStatusCompletedEvent(
        long userId,
        string phoneNumber,
        string firstName,
        string? lastName,
        //string userName,
        bool hasPassword,
        bool isUserLocked,
        Guid correlationId)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        HasPassword = hasPassword;
        IsUserLocked = isUserLocked;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public string FirstName { get; }
    public bool HasPassword { get; }
    public bool IsUserLocked { get; }
    public string? LastName { get; }
    public string PhoneNumber { get; }

    public long UserId { get; }
}
