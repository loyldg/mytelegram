// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessage : IObject
{
    BitArray Flags { get; set; }
    int Id { get; set; }

}
