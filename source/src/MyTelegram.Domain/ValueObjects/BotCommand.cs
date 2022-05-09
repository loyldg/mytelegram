namespace MyTelegram.Domain.ValueObjects;

public class BotCommand : ValueObject
{
    //public BotCommand()
    //{

    //}

    //[Newtonsoft.Json.JsonConstructor]
    //[JsonConstructor]
    public BotCommand(string command,
        string description)
    {
        Command = command;
        Description = description;
    }

    public string Command { get; }
    public string Description { get; }
}
public class InboxItem : ValueObject
{
    public InboxItem(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        InboxMessageId = inboxMessageId;
    }

    public int InboxMessageId { get; }

    public long InboxOwnerPeerId { get; }
}