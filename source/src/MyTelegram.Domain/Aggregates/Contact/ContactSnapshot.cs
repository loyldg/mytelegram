namespace MyTelegram.Domain.Aggregates.Contact;

public class ContactSnapshot : ISnapshot
{
    public ContactSnapshot(
        long selfUserId,
        long targetUserId,
        string phone,
        string firstName,
        string? lastName,
        long? photoId
    )
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        PhotoId = photoId;
    }

    public string FirstName { get; }
    public string? LastName { get; }
    public long? PhotoId { get; }
    public string Phone { get; }

    public long SelfUserId { get; }
    public long TargetUserId { get; }
}
