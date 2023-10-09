namespace MyTelegram.Domain.Events.User;

public class UserNameUpdatedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    public UserNameUpdatedEvent(RequestInfo requestInfo,
        UserItem userItem,
        string? oldUserName) : base(requestInfo)
    {
        UserItem = userItem;
        OldUserName = oldUserName;

    }

    public string? OldUserName { get; }

    public UserItem UserItem { get; }

}