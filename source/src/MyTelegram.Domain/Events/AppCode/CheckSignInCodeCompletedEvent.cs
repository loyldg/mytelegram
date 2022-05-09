namespace MyTelegram.Domain.Events.AppCode;

public class CheckSignInCodeCompletedEvent : AggregateEvent<AppCodeAggregate, AppCodeId>, IHasCorrelationId
{
    public CheckSignInCodeCompletedEvent(RequestInfo request,
        bool isCodeValid,
        long userId,
        //long accessHash,
        //string phoneNumber,
        //string firstName,
        //string lastName,
        Guid correlationId) //: base(request)
    {
        Request = request;
        IsCodeValid = isCodeValid;
        UserId = userId;
        //AccessHash = accessHash;
        //PhoneNumber = phoneNumber;
        //FirstName = firstName;
        //LastName = lastName;
        CorrelationId = correlationId;
    }

    public RequestInfo Request { get; }
    public bool IsCodeValid { get; }

    public long UserId { get; }

    //public long AccessHash { get; }
    //public string PhoneNumber { get; }
    //public string FirstName { get; }
    //public string LastName { get; }
    public Guid CorrelationId { get; }
}
