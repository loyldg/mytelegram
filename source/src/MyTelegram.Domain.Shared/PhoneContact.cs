namespace MyTelegram;

public class PhoneContact(
    long userId,
    string phone,
    string firstName,
    string? lastName,
    long clientId)
{
    public long ClientId { get; } = clientId;
    public string FirstName { get; } = firstName;
    public string? LastName { get; } = lastName;
    public string Phone { get; } = phone;

    public long UserId { get; } = userId;
}