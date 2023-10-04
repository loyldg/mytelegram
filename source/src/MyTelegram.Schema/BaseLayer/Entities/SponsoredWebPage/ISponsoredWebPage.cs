// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/SponsoredWebPage" />
///</summary>
public interface ISponsoredWebPage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string SiteName { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? Photo { get; set; }
}
