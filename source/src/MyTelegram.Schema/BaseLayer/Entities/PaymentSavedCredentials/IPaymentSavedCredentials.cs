// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Saved payment credentials
/// See <a href="https://corefork.telegram.org/constructor/PaymentSavedCredentials" />
///</summary>
public interface IPaymentSavedCredentials : IObject
{
    ///<summary>
    /// Card ID
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Title
    ///</summary>
    string Title { get; set; }
}
