// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://instantview.telegram.org/">Instant View</a> webpage preview
/// See <a href="https://corefork.telegram.org/constructor/WebPage" />
///</summary>
public interface IWebPage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }
}
