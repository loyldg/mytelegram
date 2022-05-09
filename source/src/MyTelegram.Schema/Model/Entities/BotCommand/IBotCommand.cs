// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotCommand : IObject
{
    string Command { get; set; }
    string Description { get; set; }

}
