// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Peer returned after resolving a <code>@username</code>
/// See <a href="https://corefork.telegram.org/constructor/contacts.ResolvedPeer" />
///</summary>
[JsonDerivedType(typeof(TResolvedPeer), nameof(TResolvedPeer))]
public interface IResolvedPeer : IObject
{
    ///<summary>
    /// The peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

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
