namespace MyTelegram.Domain.Events.UserName;

public class UserNameCreatedEvent : AggregateEvent<UserNameAggregate, UserNameId>
{
    public long UserId { get; }
    public string UserName { get; }

    public UserNameCreatedEvent(long userId, string userName)
    {
        UserId = userId;
        UserName = userName;
    }
}