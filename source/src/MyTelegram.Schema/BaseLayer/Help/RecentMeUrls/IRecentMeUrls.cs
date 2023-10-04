// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Recent t.me URLs
/// See <a href="https://corefork.telegram.org/constructor/help.RecentMeUrls" />
///</summary>
public interface IRecentMeUrls : IObject
{
    ///<summary>
    /// URLs
    /// See <a href="https://corefork.telegram.org/type/RecentMeUrl" />
    ///</summary>
    TVector<MyTelegram.Schema.IRecentMeUrl> Urls { get; set; }

    ///<summary>
    /// Chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
