namespace MyTelegram.Domain.Events.User;

public class UserSupportHasSetEvent : AggregateEvent<UserAggregate, UserId>
{
    public UserSupportHasSetEvent(bool support)
    {
        Support = support;
    }

    public bool Support { get; }
}
