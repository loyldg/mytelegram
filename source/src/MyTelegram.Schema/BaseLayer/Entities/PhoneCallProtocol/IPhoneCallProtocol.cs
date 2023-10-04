// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone call protocol
/// See <a href="https://corefork.telegram.org/constructor/PhoneCallProtocol" />
///</summary>
public interface IPhoneCallProtocol : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether to allow P2P connection to the other participant
    ///</summary>
    bool UdpP2p { get; set; }

    ///<summary>
    /// Whether to allow connection to the other participants through the reflector servers
    ///</summary>
    bool UdpReflector { get; set; }

    ///<summary>
    /// Minimum layer for remote libtgvoip
    ///</summary>
    int MinLayer { get; set; }

    ///<summary>
    /// Maximum layer for remote libtgvoip
    ///</summary>
    int MaxLayer { get; set; }
}
