// ReSharper disable All

namespace MyTelegram.Schema;

public interface IRequestPeerType : IObject
{
    BitArray Flags { get; set; }
}
