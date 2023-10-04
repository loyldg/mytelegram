// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a table in an <a href="https://instantview.telegram.org/">instant view table</a>
/// See <a href="https://corefork.telegram.org/constructor/PageTableCell" />
///</summary>
public interface IPageTableCell : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Is this element part of the column header
    ///</summary>
    bool Header { get; set; }

    ///<summary>
    /// Horizontally centered block
    ///</summary>
    bool AlignCenter { get; set; }

    ///<summary>
    /// Right-aligned block
    ///</summary>
    bool AlignRight { get; set; }

    ///<summary>
    /// Vertically centered block
    ///</summary>
    bool ValignMiddle { get; set; }

    ///<summary>
    /// Block vertically-aligned to the bottom
    ///</summary>
    bool ValignBottom { get; set; }

    ///<summary>
    /// Content
    /// See <a href="https://corefork.telegram.org/type/RichText" />
    ///</summary>
    MyTelegram.Schema.IRichText? Text { get; set; }

    ///<summary>
    /// For how many columns should this cell extend
    ///</summary>
    int? Colspan { get; set; }

    ///<summary>
    /// For how many rows should this cell extend
    ///</summary>
    int? Rowspan { get; set; }
}
