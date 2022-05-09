namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameSnapshot : ISnapshot
{
    public UserNameSnapshot(string userName,
        bool isDeleted)
    {
        UserName = userName;
        IsDeleted = isDeleted;
    }

    public bool IsDeleted { get; }

    public string UserName { get; }
}
