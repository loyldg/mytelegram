// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains the link that must be used to open a <a href="https://corefork.telegram.org/api/bots/webapps#named-bot-web-apps">named bot web app</a>.
/// See <a href="https://corefork.telegram.org/constructor/AppWebViewResult" />
///</summary>
[JsonDerivedType(typeof(TAppWebViewResultUrl), nameof(TAppWebViewResultUrl))]
public interface IAppWebViewResult : IObject
{
    ///<summary>
    /// The URL to open
    ///</summary>
    string Url { get; set; }
}
