// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDraftMessage : IObject
{
    BitArray Flags { get; set; }

}
