// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPeerBlocked : IObject
{
    MyTelegram.Schema.IPeer PeerId { get; set; }
    int Date { get; set; }

}
