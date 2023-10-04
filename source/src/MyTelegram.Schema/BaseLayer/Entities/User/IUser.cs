// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a user.
/// See <a href="https://corefork.telegram.org/constructor/User" />
///</summary>
public interface IUser : IObject
{
    ///<summary>
    /// ID of the user
    ///</summary>
    long Id { get; set; }
}
