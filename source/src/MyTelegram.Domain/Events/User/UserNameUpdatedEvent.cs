namespace MyTelegram.Domain.Events.User;

public class UserNameUpdatedEvent : RequestAggregateEvent<UserAggregate, UserId>, IHasCorrelationId
{
    public UserNameUpdatedEvent(long reqMsgId,
        UserItem userItem,
        string? oldUserName,
        Guid correlationId) : base(reqMsgId)
    {
        UserItem = userItem;
        OldUserName = oldUserName;
        CorrelationId = correlationId;
    }

    public string? OldUserName { get; }

    public UserItem UserItem { get; }
    public Guid CorrelationId { get; }
}
