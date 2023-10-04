// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contact status: online / offline.
/// See <a href="https://corefork.telegram.org/constructor/ContactStatus" />
///</summary>
public interface IContactStatus : IObject
{
    ///<summary>
    /// User identifier
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Online status
    /// See <a href="https://corefork.telegram.org/type/UserStatus" />
    ///</summary>
    MyTelegram.Schema.IUserStatus Status { get; set; }
}
