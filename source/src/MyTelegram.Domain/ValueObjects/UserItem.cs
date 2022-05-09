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

    public long AccessHash { get; }
    public string FirstName { get; }
    public string? LastName { get; }

    public string Phone { get; }
    public byte[]? ProfilePhoto { get; }

    public long UserId { get; }
    public string? UserName { get; }
}
