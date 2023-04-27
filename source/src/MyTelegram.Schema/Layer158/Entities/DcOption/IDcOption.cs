// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDcOption : IObject
{
    BitArray Flags { get; set; }
    bool Ipv6 { get; set; }
    bool MediaOnly { get; set; }
    bool TcpoOnly { get; set; }
    bool Cdn { get; set; }
    bool Static { get; set; }
    bool ThisPortOnly { get; set; }
    int Id { get; set; }
    string IpAddress { get; set; }
    int Port { get; set; }
    byte[]? Secret { get; set; }
}
