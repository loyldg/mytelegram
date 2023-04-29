// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPage : IObject
{
    BitArray Flags { get; set; }
    bool Part { get; set; }
    bool Rtl { get; set; }
    bool V2 { get; set; }
    string Url { get; set; }
    TVector<Schema.IPageBlock> Blocks { get; set; }
    TVector<Schema.IPhoto> Photos { get; set; }
    TVector<Schema.IDocument> Documents { get; set; }
    int? Views { get; set; }
}
