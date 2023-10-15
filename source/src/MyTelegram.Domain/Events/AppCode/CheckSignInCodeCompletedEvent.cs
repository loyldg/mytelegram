namespace MyTelegram.Domain.Events.AppCode;

public class CheckSignInCodeCompletedEvent : RequestAggregateEvent2<AppCodeAggregate, AppCodeId>
{
    public CheckSignInCodeCompletedEvent(RequestInfo requestInfo,
        bool isCodeValid,
        long userId//,
        //long accessHash,
        //string phoneNumber,
        //string firstName,
        //string lastName,
        //Guid correlationId
    ) : base(requestInfo)
    {
        IsCodeValid = isCodeValid;
        UserId = userId;
        //AccessHash = accessHash;
        //PhoneNumber = phoneNumber;
        //FirstName = firstName;
        //LastName = lastName;
        //
    }

    public bool IsCodeValid { get; }

    public long UserId { get; }

    //public long AccessHash { get; }
    //public string PhoneNumber { get; }
    //public string FirstName { get; }
    //public string LastName { get; }
    //
}