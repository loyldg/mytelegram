// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://instantview.telegram.org/">Instant view</a> page
/// See <a href="https://corefork.telegram.org/constructor/Page" />
///</summary>
public interface IPage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Indicates that not full page preview is available to the client and it will need to fetch full Instant View from the server using <a href="https://corefork.telegram.org/method/messages.getWebPagePreview">messages.getWebPagePreview</a>.
    ///</summary>
    bool Part { get; set; }

    ///<summary>
    /// Whether the page contains RTL text
    ///</summary>
    bool Rtl { get; set; }

    ///<summary>
    /// Whether this is an <a href="https://instantview.telegram.org/docs#what-39s-new-in-2-0">IV v2</a> page
    ///</summary>
    bool V2 { get; set; }

    ///<summary>
    /// Original page HTTP URL
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Page elements (like with HTML elements, only as TL constructors)
    /// See <a href="https://corefork.telegram.org/type/PageBlock" />
    ///</summary>
    TVector<MyTelegram.Schema.IPageBlock> Blocks { get; set; }

    ///<summary>
    /// Photos in page
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    TVector<MyTelegram.Schema.IPhoto> Photos { get; set; }

    ///<summary>
    /// Media in page
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    TVector<MyTelegram.Schema.IDocument> Documents { get; set; }

    ///<summary>
    /// View count
    ///</summary>
    int? Views { get; set; }
}
