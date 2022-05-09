namespace MyTelegram.Domain.Events.User;

public class UserProfileUpdatedEvent : RequestAggregateEvent<UserAggregate, UserId>
{
    public UserProfileUpdatedEvent(long reqMsgId,
        long userId,
        string? firstName,
        string? lastName,
        string? about) : base(reqMsgId)
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
