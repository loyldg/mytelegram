namespace MyTelegram.Domain.Events.AppCode;

public class CheckSignInCodeCompletedEvent : AggregateEvent<AppCodeAggregate, AppCodeId>, IHasCorrelationId
{
    public CheckSignInCodeCompletedEvent(RequestInfo requestInfo,
        bool isCodeValid,
        long userId,
        //long accessHash,
        //string phoneNumber,
        //string firstName,
        //string lastName,
        Guid correlationId) //: base(requestInfo)
    {
        RequestInfo = requestInfo;
        IsCodeValid = isCodeValid;
        UserId = userId;
        //AccessHash = accessHash;
        //PhoneNumber = phoneNumber;
        //FirstName = firstName;
        //LastName = lastName;
        CorrelationId = correlationId;
    }

    public RequestInfo RequestInfo { get; }
    public bool IsCodeValid { get; }

    public long UserId { get; }

    //public long AccessHash { get; }
    //public string PhoneNumber { get; }
    //public string FirstName { get; }
    //public string LastName { get; }
    public Guid CorrelationId { get; }
}
