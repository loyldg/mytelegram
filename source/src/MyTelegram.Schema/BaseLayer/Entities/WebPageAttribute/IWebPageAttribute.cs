// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Webpage attributes
/// See <a href="https://corefork.telegram.org/constructor/WebPageAttribute" />
///</summary>
[JsonDerivedType(typeof(TWebPageAttributeTheme), nameof(TWebPageAttributeTheme))]
[JsonDerivedType(typeof(TWebPageAttributeStory), nameof(TWebPageAttributeStory))]
public interface IWebPageAttribute : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
