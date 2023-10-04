// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about an inline message sent by a <a href="https://core.telegram.org/bots/webapps">Web App</a> on behalf of a user.
/// See <a href="https://corefork.telegram.org/constructor/WebViewMessageSent" />
///</summary>
public interface IWebViewMessageSent : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Message ID
    /// See <a href="https://corefork.telegram.org/type/InputBotInlineMessageID" />
    ///</summary>
    MyTelegram.Schema.IInputBotInlineMessageID? MsgId { get; set; }
}
