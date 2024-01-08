// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about a single <a href="https://corefork.telegram.org/api/boost">boost slot »</a>.
/// See <a href="https://corefork.telegram.org/constructor/MyBoost" />
///</summary>
[JsonDerivedType(typeof(TMyBoost), nameof(TMyBoost))]
public interface IMyBoost : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/boost">Boost slot ID »</a>
    ///</summary>
    int Slot { get; set; }

    ///<summary>
    /// If set, indicates this slot is currently occupied, i.e. we are <a href="https://corefork.telegram.org/api/boost">boosting</a> this peer.  <br>Note that we can assign multiple boost slots to the same peer.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? Peer { get; set; }

    ///<summary>
    /// When (unixtime) we started boosting the <code>peer</code>, <code>0</code> otherwise.
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Indicates the (unixtime) expiration date of the boost in <code>peer</code> (<code>0</code> if <code>peer</code> is not set).
    ///</summary>
    int Expires { get; set; }

    ///<summary>
    /// If <code>peer</code> is set, indicates the (unixtime) date after which this boost can be reassigned to another channel.
    ///</summary>
    int? CooldownUntilDate { get; set; }
}
