// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputBotInlineResult : IObject
{
    string Id { get; set; }
    MyTelegram.Schema.IInputBotInlineMessage SendMessage { get; set; }

}
