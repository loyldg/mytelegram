// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Takeout info
/// See <a href="https://corefork.telegram.org/constructor/account.Takeout" />
///</summary>
public interface ITakeout : IObject
{
    ///<summary>
    /// Takeout ID
    ///</summary>
    long Id { get; set; }
}
