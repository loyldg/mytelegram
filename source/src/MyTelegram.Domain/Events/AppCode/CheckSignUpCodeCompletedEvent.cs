namespace MyTelegram.Domain.Events.AppCode;

public class CheckSignUpCodeCompletedEvent : RequestAggregateEvent2<AppCodeAggregate, AppCodeId>
{
    //public AppCodeCheckCompletedEvent(long reqMsgId,
    //    bool isCodeValid,
    //    string errorMessage,
    //    Guid correlationId) : base(reqMsgId)
    //{
    //    IsCodeValid = isCodeValid;
    //    ErrorMessage = errorMessage;
    //    
    //}

    //public bool IsCodeValid { get; }
    //public string ErrorMessage { get; }
    //
    public CheckSignUpCodeCompletedEvent(RequestInfo requestInfo,
        bool isCodeValid,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName) : base(requestInfo)
    {
        IsCodeValid = isCodeValid;
        UserId = userId;
        AccessHash = accessHash;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
    }

    public long AccessHash { get; }
    public string FirstName { get; }

    public bool IsCodeValid { get; }
    public string? LastName { get; }
    public string PhoneNumber { get; }
    public long UserId { get; }
}