// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Shipping options
/// See <a href="https://corefork.telegram.org/constructor/ShippingOption" />
///</summary>
[JsonDerivedType(typeof(TShippingOption), nameof(TShippingOption))]
public interface IShippingOption : IObject
{
    ///<summary>
    /// Option ID
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// List of price portions
    /// See <a href="https://corefork.telegram.org/type/LabeledPrice" />
    ///</summary>
    TVector<MyTelegram.Schema.ILabeledPrice> Prices { get; set; }
}
