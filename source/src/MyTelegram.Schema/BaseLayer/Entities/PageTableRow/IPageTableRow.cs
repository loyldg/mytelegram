// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Table row
/// See <a href="https://corefork.telegram.org/constructor/PageTableRow" />
///</summary>
public interface IPageTableRow : IObject
{
    ///<summary>
    /// Table cells
    /// See <a href="https://corefork.telegram.org/type/PageTableCell" />
    ///</summary>
    TVector<MyTelegram.Schema.IPageTableCell> Cells { get; set; }
}
