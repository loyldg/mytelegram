// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageTableCell : IObject
{
    BitArray Flags { get; set; }
    bool Header { get; set; }
    bool AlignCenter { get; set; }
    bool AlignRight { get; set; }
    bool ValignMiddle { get; set; }
    bool ValignBottom { get; set; }
    Schema.IRichText? Text { get; set; }
    int? Colspan { get; set; }
    int? Rowspan { get; set; }
}
