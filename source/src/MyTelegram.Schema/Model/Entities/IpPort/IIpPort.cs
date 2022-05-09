// ReSharper disable All

namespace MyTelegram.Schema;

public interface IIpPort : IObject
{
    int Ipv4 { get; set; }
    int Port { get; set; }

}
