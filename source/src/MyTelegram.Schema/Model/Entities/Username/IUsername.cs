// ReSharper disable All

namespace MyTelegram.Schema;

public interface IUsername : IObject
{
    BitArray Flags { get; set; }
    bool Editable { get; set; }
    bool Active { get; set; }
    string Username { get; set; }

}
