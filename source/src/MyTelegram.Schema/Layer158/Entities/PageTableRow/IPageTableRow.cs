// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageTableRow : IObject
{
    TVector<Schema.IPageTableCell> Cells { get; set; }
}
