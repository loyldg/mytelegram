// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a peer that can be used to send messages
/// See <a href="https://corefork.telegram.org/constructor/SendAsPeer" />
///</summary>
public interface ISendAsPeer : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether a Telegram Premium account is required to send messages as this peer
    ///</summary>
    bool PremiumRequired { get; set; }

    ///<summary>
    /// Peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }
}
