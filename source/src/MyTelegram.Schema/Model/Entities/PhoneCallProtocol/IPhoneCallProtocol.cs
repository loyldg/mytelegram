// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPhoneCallProtocol : IObject
{
    BitArray Flags { get; set; }
    bool UdpP2p { get; set; }
    bool UdpReflector { get; set; }
    int MinLayer { get; set; }
    int MaxLayer { get; set; }
    TVector<string> LibraryVersions { get; set; }

}
