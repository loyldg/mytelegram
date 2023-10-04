// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Temporary password
/// See <a href="https://corefork.telegram.org/constructor/account.TmpPassword" />
///</summary>
public interface ITmpPassword : IObject
{
    ///<summary>
    /// Temporary password
    ///</summary>
    byte[] TmpPassword { get; set; }

    ///<summary>
    /// Validity period
    ///</summary>
    int ValidUntil { get; set; }
}
