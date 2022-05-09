namespace MyTelegram.Domain.Aggregates.User;

public class UserSnapshot : ISnapshot
{
    public UserSnapshot(long userId,
        bool isOnline,
        long accessHash,
        string firstName,
        string? lastName,
        string phoneNumber,
        string? userName,
        bool hasPassword,
        byte[]? photo)
    {
        UserId = userId;
        IsOnline = isOnline;
        AccessHash = accessHash;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        UserName = userName;
        HasPassword = hasPassword;
        Photo = photo;
    }

    public long AccessHash { get; }
    public string FirstName { get; }
    public bool HasPassword { get; }
    public bool IsOnline { get; }
    public string? LastName { get; }
    public string PhoneNumber { get; }
    public byte[]? Photo { get; }
    public long UserId { get; }
    public string? UserName { get; }
}
