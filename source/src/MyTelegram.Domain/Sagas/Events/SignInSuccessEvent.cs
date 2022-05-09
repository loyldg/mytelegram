namespace MyTelegram.Domain.Sagas.Events;

public class SignInSuccessEvent : AggregateEvent<SignInSaga, SignInSagaId>
{
    public SignInSuccessEvent(long reqMsgId,
        long tempAuthKeyId,
        long permAuthKeyId,
        long userId,
        bool signUpRequired,
        string phoneNumber,
        string firstName,
        string? lastName,
        bool hasPassword)
    {
        ReqMsgId = reqMsgId;
        TempAuthKeyId = tempAuthKeyId;
        PermAuthKeyId = permAuthKeyId;
        UserId = userId;
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

    public long ReqMsgId { get; }
    public bool SignUpRequired { get; }
    public long TempAuthKeyId { get; }
    public long UserId { get; }
}
