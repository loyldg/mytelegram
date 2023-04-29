namespace MyTelegram.Domain.ValueObjects;

public class UserItem : ValueObject
{
    public UserItem(
        long userId,
        long accessHash,
        string phone,
        string firstName,
        string? lastName,
        string? userName,
        byte[]? profilePhoto
    )
    {
        UserId = userId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        AccessHash = accessHash;
        UserName = userName;
        ProfilePhoto = profilePhoto;
    }

    public long AccessHash { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }

    public string Phone { get; private set; }
    public byte[]? ProfilePhoto { get; private set; }

    public long UserId { get; private set; }
    public string? UserName { get; private set; }
}
