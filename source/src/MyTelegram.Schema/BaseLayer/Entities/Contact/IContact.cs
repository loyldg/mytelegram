// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A contact of the current user.
/// See <a href="https://corefork.telegram.org/constructor/Contact" />
///</summary>
[JsonDerivedType(typeof(TContact), nameof(TContact))]
public interface IContact : IObject
{
    ///<summary>
    /// User identifier
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Current user is in the user's contact list
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool Mutual { get; set; }
}
