// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPollAnswer : IObject
{
    string Text { get; set; }
    string Option { get; set; }
}
