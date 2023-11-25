// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Result of a query to an inline bot
/// See <a href="https://corefork.telegram.org/constructor/messages.BotResults" />
///</summary>
[JsonDerivedType(typeof(TBotResults), nameof(TBotResults))]
public interface IBotResults : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the result is a picture gallery
    ///</summary>
    bool Gallery { get; set; }

    ///<summary>
    /// Query ID
    ///</summary>
    long QueryId { get; set; }

    ///<summary>
    /// The next offset to use when navigating through results
    ///</summary>
    string? NextOffset { get; set; }

    ///<summary>
    /// Shown as a button on top of the remaining inline result list; if clicked, redirects the user to a private chat with the bot with the specified start parameter.
    /// See <a href="https://corefork.telegram.org/type/InlineBotSwitchPM" />
    ///</summary>
    MyTelegram.Schema.IInlineBotSwitchPM? SwitchPm { get; set; }

    ///<summary>
    /// Shown as a button on top of the remaining inline result list; if clicked, opens the specified <a href="https://corefork.telegram.org/api/bots/webapps#simple-web-apps">bot web app</a>.
    /// See <a href="https://corefork.telegram.org/type/InlineBotWebView" />
    ///</summary>
    MyTelegram.Schema.IInlineBotWebView? SwitchWebview { get; set; }

    ///<summary>
    /// The results
    /// See <a href="https://corefork.telegram.org/type/BotInlineResult" />
    ///</summary>
    TVector<MyTelegram.Schema.IBotInlineResult> Results { get; set; }

    ///<summary>
    /// Caching validity of the results
    ///</summary>
    int CacheTime { get; set; }

    ///<summary>
    /// Users mentioned in the results
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
