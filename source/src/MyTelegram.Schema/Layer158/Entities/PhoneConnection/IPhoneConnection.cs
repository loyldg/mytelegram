// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPhoneConnection : IObject
{
    BitArray Flags { get; set; }
    long Id { get; set; }
    string Ip { get; set; }
    string Ipv6 { get; set; }
    int Port { get; set; }
}
