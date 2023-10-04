// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a sent inline message from the perspective of a bot
/// See <a href="https://corefork.telegram.org/constructor/InputBotInlineMessageID" />
///</summary>
public interface IInputBotInlineMessageID : IObject
{
    ///<summary>
    /// DC ID to use when working with this inline message
    ///</summary>
    int DcId { get; set; }

    ///<summary>
    /// Access hash of message
    ///</summary>
    long AccessHash { get; set; }
}
