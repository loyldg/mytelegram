// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAccessPointRule : IObject
{
    string PhonePrefixRules { get; set; }
    int DcId { get; set; }
    TVector<TIpPort> Ips { get; set; }

}
