// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Information for connection to data center.
/// See <a href="https://corefork.telegram.org/constructor/DcOption" />
///</summary>
public interface IDcOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the specified IP is an IPv6 address
    ///</summary>
    bool Ipv6 { get; set; }

    ///<summary>
    /// Whether this DC should only be used to <a href="https://corefork.telegram.org/api/files">download or upload files</a>
    ///</summary>
    bool MediaOnly { get; set; }

    ///<summary>
    /// Whether this DC only supports connection with <a href="https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation">transport obfuscation</a>
    ///</summary>
    bool TcpoOnly { get; set; }

    ///<summary>
    /// Whether this is a <a href="https://corefork.telegram.org/cdn">CDN DC</a>.
    ///</summary>
    bool Cdn { get; set; }

    ///<summary>
    /// If set, this IP should be used when connecting through a proxy
    ///</summary>
    bool Static { get; set; }

    ///<summary>
    /// If set, clients must connect using only the specified port, without trying any other port.
    ///</summary>
    bool ThisPortOnly { get; set; }

    ///<summary>
    /// DC ID
    ///</summary>
    int Id { get; set; }

    ///<summary>
    /// IP address of DC
    ///</summary>
    string IpAddress { get; set; }

    ///<summary>
    /// Port
    ///</summary>
    int Port { get; set; }

    ///<summary>
    /// If the <code>tcpo_only</code> flag is set, specifies the secret to use when connecting using <a href="https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation">transport obfuscation</a>
    ///</summary>
    byte[]? Secret { get; set; }
}
