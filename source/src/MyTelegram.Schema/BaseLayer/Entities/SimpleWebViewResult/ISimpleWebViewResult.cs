// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains the webview URL with appropriate theme parameters added
/// See <a href="https://corefork.telegram.org/constructor/SimpleWebViewResult" />
///</summary>
[JsonDerivedType(typeof(TSimpleWebViewResultUrl), nameof(TSimpleWebViewResultUrl))]
public interface ISimpleWebViewResult : IObject
{
    ///<summary>
    /// URL
    ///</summary>
    string Url { get; set; }
}
