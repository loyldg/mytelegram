// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Info on users from the current user's black list.
/// See <a href="https://corefork.telegram.org/constructor/contacts.Blocked" />
///</summary>
[JsonDerivedType(typeof(TBlocked), nameof(TBlocked))]
[JsonDerivedType(typeof(TBlockedSlice), nameof(TBlockedSlice))]
public interface IBlocked : IObject
{
    ///<summary>
    /// List of blocked users
    /// See <a href="https://corefork.telegram.org/type/PeerBlocked" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeerBlocked> Blocked { get; set; }

    ///<summary>
    /// Blocked chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// List of users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
