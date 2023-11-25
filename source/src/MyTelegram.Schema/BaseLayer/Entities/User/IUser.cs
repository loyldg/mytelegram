// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a user.
/// See <a href="https://corefork.telegram.org/constructor/User" />
///</summary>
[JsonDerivedType(typeof(TUserEmpty), nameof(TUserEmpty))]
[JsonDerivedType(typeof(TUser), nameof(TUser))]
public interface IUser : IObject
{
    ///<summary>
    /// ID of the user
    ///</summary>
    long Id { get; set; }
}
