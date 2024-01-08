// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Specifies an <a href="https://corefork.telegram.org/api/bots/webapps#inline-mode-mini-apps">inline mode mini app</a> button, shown on top of the inline query results list.
/// See <a href="https://corefork.telegram.org/constructor/InlineBotWebView" />
///</summary>
[JsonDerivedType(typeof(TInlineBotWebView), nameof(TInlineBotWebView))]
public interface IInlineBotWebView : IObject
{
    ///<summary>
    /// Text of the button
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// Webapp URL
    ///</summary>
    string Url { get; set; }
}
