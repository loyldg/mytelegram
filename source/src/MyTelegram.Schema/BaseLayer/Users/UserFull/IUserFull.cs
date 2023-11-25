// ReSharper disable All

namespace MyTelegram.Schema.Users;

///<summary>
/// Full user information, with attached context peers for reactions
/// See <a href="https://corefork.telegram.org/constructor/users.UserFull" />
///</summary>
[JsonDerivedType(typeof(TUserFull), nameof(TUserFull))]
public interface IUserFull : IObject
{
    ///<summary>
    /// Full user information
    /// See <a href="https://corefork.telegram.org/type/UserFull" />
    ///</summary>
    MyTelegram.Schema.IUserFull FullUser { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
