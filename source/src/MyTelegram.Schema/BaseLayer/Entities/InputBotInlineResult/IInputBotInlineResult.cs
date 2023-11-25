// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline bot result
/// See <a href="https://corefork.telegram.org/constructor/InputBotInlineResult" />
///</summary>
[JsonDerivedType(typeof(TInputBotInlineResult), nameof(TInputBotInlineResult))]
[JsonDerivedType(typeof(TInputBotInlineResultPhoto), nameof(TInputBotInlineResultPhoto))]
[JsonDerivedType(typeof(TInputBotInlineResultDocument), nameof(TInputBotInlineResultDocument))]
[JsonDerivedType(typeof(TInputBotInlineResultGame), nameof(TInputBotInlineResultGame))]
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
