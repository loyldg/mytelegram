namespace MyTelegram.Domain.ValueObjects;

public class ChatMember : ValueObject
{
    public ChatMember(long userId,
        long inviterId,
        int date)
    {
        UserId = userId;
        InviterId = inviterId;
        Date = date;
    }

    public int Date { get; }
    public long InviterId { get; }

    public long UserId { get; }
}
