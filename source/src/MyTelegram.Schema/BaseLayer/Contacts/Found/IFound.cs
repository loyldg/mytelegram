// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Object contains info on users found by name substring and auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/contacts.Found" />
///</summary>
public interface IFound : IObject
{
    ///<summary>
    /// Personalized results
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer> MyResults { get; set; }

    ///<summary>
    /// List of found user identifiers
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer> Results { get; set; }

    ///<summary>
    /// Found chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// List of users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
