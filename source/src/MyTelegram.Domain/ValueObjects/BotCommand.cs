namespace MyTelegram.Domain.ValueObjects;

public class BotCommand : ValueObject
{
    public BotCommand(string command,
        string description)
    {
        Command = command;
        Description = description;
    }

    public string Command { get; private set; }
    public string Description { get; private set; }
}