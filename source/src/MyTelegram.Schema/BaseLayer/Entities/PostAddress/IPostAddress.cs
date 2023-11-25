// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Shipping address
/// See <a href="https://corefork.telegram.org/constructor/PostAddress" />
///</summary>
[JsonDerivedType(typeof(TPostAddress), nameof(TPostAddress))]
public interface IPostAddress : IObject
{
    ///<summary>
    /// First line for the address
    ///</summary>
    string StreetLine1 { get; set; }

    ///<summary>
    /// Second line for the address
    ///</summary>
    string StreetLine2 { get; set; }

    ///<summary>
    /// City
    ///</summary>
    string City { get; set; }

    ///<summary>
    /// State, if applicable (empty otherwise)
    ///</summary>
    string State { get; set; }

    ///<summary>
    /// ISO 3166-1 alpha-2 country code
    ///</summary>
    string CountryIso2 { get; set; }

    ///<summary>
    /// Address post code
    ///</summary>
    string PostCode { get; set; }
}
