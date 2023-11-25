// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// The email code that was sent
/// See <a href="https://corefork.telegram.org/constructor/account.SentEmailCode" />
///</summary>
[JsonDerivedType(typeof(TSentEmailCode), nameof(TSentEmailCode))]
public interface ISentEmailCode : IObject
{
    ///<summary>
    /// The email (to which the code was sent) must match this <a href="https://corefork.telegram.org/api/pattern">pattern</a>
    ///</summary>
    string EmailPattern { get; set; }

    ///<summary>
    /// The length of the verification code
    ///</summary>
    int Length { get; set; }
}
