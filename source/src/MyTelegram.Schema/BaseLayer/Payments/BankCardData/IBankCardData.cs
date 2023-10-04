// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Credit card info, provided by the card's bank(s)
/// See <a href="https://corefork.telegram.org/constructor/payments.BankCardData" />
///</summary>
public interface IBankCardData : IObject
{
    ///<summary>
    /// Credit card title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Info URL(s) provided by the card's bank(s)
    /// See <a href="https://corefork.telegram.org/type/BankCardOpenUrl" />
    ///</summary>
    TVector<MyTelegram.Schema.IBankCardOpenUrl> OpenUrls { get; set; }
}
