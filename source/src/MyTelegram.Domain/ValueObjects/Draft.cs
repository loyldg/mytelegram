namespace MyTelegram.Domain.ValueObjects;

public class Draft : ValueObject
{
    public Draft(string message,
        bool noWebpage,
        int? replyToMsgId,
        int date,
        byte[]? entities = null,
        int? topMsgId = null
    )
    {
        Message = message;
        NoWebpage = noWebpage;
        ReplyToMsgId = replyToMsgId;
        Date = date;
        Entities = entities;
        TopMsgId = topMsgId;
    }
    public string Message { get; init; }
    public bool NoWebpage { get; init; }
    public int? ReplyToMsgId { get; init; }
    public int Date { get; init; }
    public byte[]? Entities { get; init; }
    public int? TopMsgId { get; init; }
}
