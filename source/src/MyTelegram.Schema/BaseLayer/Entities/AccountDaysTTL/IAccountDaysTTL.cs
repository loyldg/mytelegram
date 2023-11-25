// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Time-to-live of current account
/// See <a href="https://corefork.telegram.org/constructor/AccountDaysTTL" />
///</summary>
[JsonDerivedType(typeof(TAccountDaysTTL), nameof(TAccountDaysTTL))]
public interface IAccountDaysTTL : IObject
{
    ///<summary>
    /// This account will self-destruct in the specified number of days
    ///</summary>
    int Days { get; set; }
}
