namespace MyTelegram.Domain.Sagas.Events;

public class SignInSuccessEvent : RequestAggregateEvent2<SignInSaga, SignInSagaId>
{
    public SignInSuccessEvent(RequestInfo requestInfo,
        long tempAuthKeyId,
        long permAuthKeyId,
        long userId,
        long accessHash,
        bool signUpRequired,
        string phoneNumber,
        string firstName,
        string? lastName,
        bool hasPassword) : base(requestInfo)
    {
        TempAuthKeyId = tempAuthKeyId;
        PermAuthKeyId = permAuthKeyId;
        UserId = userId;
        AccessHash = accessHash;
        SignUpRequired = signUpRequired;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        HasPassword = hasPassword;
    }

    public string FirstName { get; }
    public bool HasPassword { get; }
    public string? LastName { get; }
    public long PermAuthKeyId { get; }
    public string PhoneNumber { get; }

    public bool SignUpRequired { get; }
    public long TempAuthKeyId { get; }
    public long UserId { get; }
    public long AccessHash { get; }
}