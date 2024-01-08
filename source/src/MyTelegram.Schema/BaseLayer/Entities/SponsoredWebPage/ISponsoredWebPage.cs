// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a sponsored website.
/// See <a href="https://corefork.telegram.org/constructor/SponsoredWebPage" />
///</summary>
[JsonDerivedType(typeof(TSponsoredWebPage), nameof(TSponsoredWebPage))]
public interface ISponsoredWebPage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Web page URL.
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Website name.
    ///</summary>
    string SiteName { get; set; }

    ///<summary>
    /// Optional image preview.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? Photo { get; set; }
}
