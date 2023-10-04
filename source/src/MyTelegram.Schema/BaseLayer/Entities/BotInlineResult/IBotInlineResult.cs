// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Results of an inline query
/// See <a href="https://corefork.telegram.org/constructor/BotInlineResult" />
///</summary>
public interface IBotInlineResult : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Result ID
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Result type (see <a href="https://corefork.telegram.org/bots/api#inlinequeryresult">bot API docs</a>)
    ///</summary>
    string Type { get; set; }

    ///<summary>
    /// Depending on the <code>type</code> and on the <a href="https://corefork.telegram.org/type/BotInlineMessage">constructor</a>, contains the caption of the media or the content of the message to be sent <strong>instead</strong> of the media
    /// See <a href="https://corefork.telegram.org/type/BotInlineMessage" />
    ///</summary>
    MyTelegram.Schema.IBotInlineMessage SendMessage { get; set; }
}
