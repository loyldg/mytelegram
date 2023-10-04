// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Credit card info URL provided by the bank
/// See <a href="https://corefork.telegram.org/constructor/BankCardOpenUrl" />
///</summary>
public interface IBankCardOpenUrl : IObject
{
    ///<summary>
    /// Info URL
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Bank name
    ///</summary>
    string Name { get; set; }
}
