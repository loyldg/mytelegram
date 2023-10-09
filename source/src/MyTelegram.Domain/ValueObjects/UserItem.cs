namespace MyTelegram.Domain.ValueObjects;

public class UserItem : ValueObject
{
    //// required for native aot serialization
    //public UserItem()
    //{

    //}

    //[Newtonsoft.Json.JsonConstructor]
    //[JsonConstructor]
    public UserItem(
        long userId,
        long accessHash,
        string phone,
        string firstName,
        string? lastName,
        string? userName//,
        //byte[]? profilePhoto
    )
    {
        UserId = userId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        AccessHash = accessHash;
        UserName = userName;
        //ProfilePhoto = profilePhoto;
    }

    public long AccessHash { get; init; }
    public string FirstName { get; init; }
    public string? LastName { get; init; }

    public string Phone { get; init; }
    public byte[]? ProfilePhoto { get; init; }

    public long UserId { get; init; }
    public string? UserName { get; init; }
}