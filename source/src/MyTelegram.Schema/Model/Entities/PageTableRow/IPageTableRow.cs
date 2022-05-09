// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageTableRow : IObject
{
    TVector<MyTelegram.Schema.IPageTableCell> Cells { get; set; }

}
