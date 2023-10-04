// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Name, ISO code, localized name and phone codes/patterns of a specific country
/// See <a href="https://corefork.telegram.org/constructor/help.Country" />
///</summary>
public interface ICountry : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this country should not be shown in the list
    ///</summary>
    bool Hidden { get; set; }

    ///<summary>
    /// ISO code of country
    ///</summary>
    string Iso2 { get; set; }

    ///<summary>
    /// Name of the country in the country's language
    ///</summary>
    string DefaultName { get; set; }

    ///<summary>
    /// Name of the country in the user's language, if different from the original name
    ///</summary>
    string? Name { get; set; }

    ///<summary>
    /// Phone codes/patterns
    /// See <a href="https://corefork.telegram.org/type/help.CountryCode" />
    ///</summary>
    TVector<MyTelegram.Schema.Help.ICountryCode> CountryCodes { get; set; }
}
