// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains the webview URL with appropriate theme and user info parameters added
/// See <a href="https://corefork.telegram.org/constructor/WebViewResult" />
///</summary>
public interface IWebViewResult : IObject
{
    ///<summary>
    /// Webview session ID
    ///</summary>
    long QueryId { get; set; }

    ///<summary>
    /// Webview URL to open
    ///</summary>
    string Url { get; set; }
}
