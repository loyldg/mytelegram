namespace MyTelegram.Domain.Sagas.Events;

public class UserSignUpSuccessEvent : RequestAggregateEvent2<UserSignUpSaga, UserSignUpSagaId>
{
    public UserSignUpSuccessEvent(RequestInfo requestInfo,
        long userId,
        string phoneNumber) : base(requestInfo)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }

    public long UserId { get; }
}
