// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Country code and phone number pattern of a specific country
/// See <a href="https://corefork.telegram.org/constructor/help.CountryCode" />
///</summary>
[JsonDerivedType(typeof(TCountryCode), nameof(TCountryCode))]
public interface ICountryCode : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// ISO country code
    ///</summary>
    string CountryCode { get; set; }

    ///<summary>
    /// Possible phone prefixes
    ///</summary>
    TVector<string>? Prefixes { get; set; }

    ///<summary>
    /// Phone patterns: for example, <code>XXX XXX XXX</code>
    ///</summary>
    TVector<string>? Patterns { get; set; }
}
