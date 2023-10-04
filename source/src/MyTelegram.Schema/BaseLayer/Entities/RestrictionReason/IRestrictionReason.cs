// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Restriction reason
/// See <a href="https://corefork.telegram.org/constructor/RestrictionReason" />
///</summary>
public interface IRestrictionReason : IObject
{
    ///<summary>
    /// Platform identifier (ios, android, wp, all, etc.), can be concatenated with a dash as separator (<code>android-ios</code>, <code>ios-wp</code>, etc)
    ///</summary>
    string Platform { get; set; }

    ///<summary>
    /// Restriction reason (<code>porno</code>, <code>terms</code>, etc.)
    ///</summary>
    string Reason { get; set; }

    ///<summary>
    /// Error message to be shown to the user
    ///</summary>
    string Text { get; set; }
}
