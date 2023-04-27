// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPeerLocated : IObject
{
    int Expires { get; set; }
}
