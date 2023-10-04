// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Recent t.me urls
/// See <a href="https://corefork.telegram.org/constructor/RecentMeUrl" />
///</summary>
public interface IRecentMeUrl : IObject
{
    ///<summary>
    /// t.me URL
    ///</summary>
    string Url { get; set; }
}
