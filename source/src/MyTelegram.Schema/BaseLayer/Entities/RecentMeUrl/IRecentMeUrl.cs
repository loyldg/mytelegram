// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Recent t.me urls
/// See <a href="https://corefork.telegram.org/constructor/RecentMeUrl" />
///</summary>
[JsonDerivedType(typeof(TRecentMeUrlUnknown), nameof(TRecentMeUrlUnknown))]
[JsonDerivedType(typeof(TRecentMeUrlUser), nameof(TRecentMeUrlUser))]
[JsonDerivedType(typeof(TRecentMeUrlChat), nameof(TRecentMeUrlChat))]
[JsonDerivedType(typeof(TRecentMeUrlChatInvite), nameof(TRecentMeUrlChatInvite))]
[JsonDerivedType(typeof(TRecentMeUrlStickerSet), nameof(TRecentMeUrlStickerSet))]
public interface IRecentMeUrl : IObject
{
    ///<summary>
    /// t.me URL
    ///</summary>
    string Url { get; set; }
}
