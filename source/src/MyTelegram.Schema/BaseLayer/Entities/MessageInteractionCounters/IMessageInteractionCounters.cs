// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Message interaction counters
/// See <a href="https://corefork.telegram.org/constructor/MessageInteractionCounters" />
///</summary>
public interface IMessageInteractionCounters : IObject
{
    ///<summary>
    /// Message ID
    ///</summary>
    int MsgId { get; set; }

    ///<summary>
    /// Views
    ///</summary>
    int Views { get; set; }

    ///<summary>
    /// Number of times this message was forwarded
    ///</summary>
    int Forwards { get; set; }
}
