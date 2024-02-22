namespace MyTelegram.Domain.Commands.Contact;

public class PhoneContact
{
    public PhoneContact(long userId,
        string phone,
        string firstName,
        string? lastName,
        long clientId
    )
    {
        UserId = userId;
        Phone = phone;
        FirstName = firstName;
        LastName = lastName;
        ClientId = clientId;
    }

    public long ClientId { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string Phone { get; }

    public long UserId { get; }
}
