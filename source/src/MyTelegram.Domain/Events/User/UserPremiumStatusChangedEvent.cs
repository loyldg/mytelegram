namespace MyTelegram.Domain.Events.User;

public class UserPremiumStatusChangedEvent : AggregateEvent<UserAggregate, UserId>
{
    public UserPremiumStatusChangedEvent(long userId,string phoneNumber, bool premium)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        Premium = premium;
    }

    public long UserId { get; }
    public string PhoneNumber { get; }
    public bool Premium { get; }
}