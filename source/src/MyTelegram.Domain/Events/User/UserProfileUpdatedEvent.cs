namespace MyTelegram.Domain.Events.User;

public class UserProfileUpdatedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    public UserProfileUpdatedEvent(RequestInfo requestInfo,
        long userId,
        string? firstName,
        string? lastName,
        string? about) : base(requestInfo)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        About = about;
    }

    public string? About { get; }
    public string? FirstName { get; }
    public string? LastName { get; }

    public long UserId { get; }
}