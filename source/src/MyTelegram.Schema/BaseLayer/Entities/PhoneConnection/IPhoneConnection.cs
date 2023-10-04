// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone call connection
/// See <a href="https://corefork.telegram.org/constructor/PhoneConnection" />
///</summary>
public interface IPhoneConnection : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Endpoint ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// IP address
    ///</summary>
    string Ip { get; set; }

    ///<summary>
    /// IPv6 address
    ///</summary>
    string Ipv6 { get; set; }

    ///<summary>
    /// Port
    ///</summary>
    int Port { get; set; }
}
