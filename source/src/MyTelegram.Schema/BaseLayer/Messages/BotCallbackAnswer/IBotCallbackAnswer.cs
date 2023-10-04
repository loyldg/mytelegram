// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Callback answer of bot
/// See <a href="https://corefork.telegram.org/constructor/messages.BotCallbackAnswer" />
///</summary>
public interface IBotCallbackAnswer : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether an alert should be shown to the user instead of a toast notification
    ///</summary>
    bool Alert { get; set; }

    ///<summary>
    /// Whether an URL is present
    ///</summary>
    bool HasUrl { get; set; }

    ///<summary>
    /// Whether to show games in WebView or in native UI.
    ///</summary>
    bool NativeUi { get; set; }

    ///<summary>
    /// Alert to show
    ///</summary>
    string? Message { get; set; }

    ///<summary>
    /// URL to open
    ///</summary>
    string? Url { get; set; }

    ///<summary>
    /// For how long should this answer be cached
    ///</summary>
    int CacheTime { get; set; }
}
