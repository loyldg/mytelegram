namespace MyTelegram.Domain.Aggregates.Contact;

public class ImportedContactSnapshot : ISnapshot
{
    //public bool AddPhonePrivacyException { get; }

    public ImportedContactSnapshot(
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName //,
        //bool addPhonePrivacyException
    )
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        //AddPhonePrivacyException = addPhonePrivacyException;
    }

    public string FirstName { get; }
    public string? LastName { get; }
    public string Phone { get; }

    public long SelfUserId { get; }
    public long TargetUserId { get; }
}
