// ReSharper disable All

namespace MyTelegram.Schema;

public interface IKeyboardButton : IObject
{
    string Text { get; set; }
}
