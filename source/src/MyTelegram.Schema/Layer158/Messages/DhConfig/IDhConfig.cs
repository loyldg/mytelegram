// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IDhConfig : IObject
{
    byte[] Random { get; set; }
}
