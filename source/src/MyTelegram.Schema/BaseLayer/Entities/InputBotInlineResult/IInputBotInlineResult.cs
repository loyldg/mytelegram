// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline bot result
/// See <a href="https://corefork.telegram.org/constructor/InputBotInlineResult" />
///</summary>
public interface IInputBotInlineResult : IObject
{
    ///<summary>
    /// Result ID
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Message to send when the result is selected
    /// See <a href="https://corefork.telegram.org/type/InputBotInlineMessage" />
    ///</summary>
    MyTelegram.Schema.IInputBotInlineMessage SendMessage { get; set; }
}
