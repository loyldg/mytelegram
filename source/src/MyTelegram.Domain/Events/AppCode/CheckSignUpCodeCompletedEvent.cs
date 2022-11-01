namespace MyTelegram.Domain.Events.AppCode;

public class CheckSignUpCodeCompletedEvent : RequestAggregateEvent2<AppCodeAggregate, AppCodeId>, IHasCorrelationId
{
    //public AppCodeCheckCompletedEvent(long reqMsgId,
    //    bool isCodeValid,
    //    string errorMessage,
    //    Guid correlationId) : base(reqMsgId)
    //{
    //    IsCodeValid = isCodeValid;
    //    ErrorMessage = errorMessage;
    //    CorrelationId = correlationId;
    //}

    //public bool IsCodeValid { get; }
    //public string ErrorMessage { get; }
    //public Guid CorrelationId { get; }
    public CheckSignUpCodeCompletedEvent(RequestInfo requestInfo,
        bool isCodeValid,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        Guid correlationId) : base(requestInfo)
    {
        IsCodeValid = isCodeValid;
        UserId = userId;
        AccessHash = accessHash;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        CorrelationId = correlationId;
    }

    public long AccessHash { get; }
    public string FirstName { get; }

    public bool IsCodeValid { get; }
    public string? LastName { get; }
    public string PhoneNumber { get; }
    public long UserId { get; }
    public Guid CorrelationId { get; }
}
