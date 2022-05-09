// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPhoneConnection : IObject
{
    long Id { get; set; }
    string Ip { get; set; }
    string Ipv6 { get; set; }
    int Port { get; set; }

}
