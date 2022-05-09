namespace MyTelegram.Domain.Events.User;

public class UserVerifiedHasSetEvent : AggregateEvent<UserAggregate, UserId>
{
    public UserVerifiedHasSetEvent(bool verified)
    {
        Verified = verified;
    }

    public bool Verified { get; }
}
