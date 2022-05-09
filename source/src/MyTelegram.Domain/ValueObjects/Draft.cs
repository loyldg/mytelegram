namespace MyTelegram.Domain.ValueObjects;

public class Draft : ValueObject
{
    public Draft(string message,
        bool noWebpage,
        int? replyToMsgId,
        int date,
        byte[]? entities = null)
    {
        Message = message;
        NoWebpage = noWebpage;
        ReplyToMsgId = replyToMsgId;
        Date = date;
        Entities = entities;
    }

    public int Date { get; }
    public byte[]? Entities { get; }
    public string Message { get; }
    public bool NoWebpage { get; }
    public int? ReplyToMsgId { get; }
}
