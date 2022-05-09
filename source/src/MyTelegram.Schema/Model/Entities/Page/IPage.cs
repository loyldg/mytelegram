// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPage : IObject
{
    BitArray Flags { get; set; }
    bool Part { get; set; }
    bool Rtl { get; set; }
    bool V2 { get; set; }
    string Url { get; set; }
    TVector<MyTelegram.Schema.IPageBlock> Blocks { get; set; }
    TVector<MyTelegram.Schema.IPhoto> Photos { get; set; }
    TVector<MyTelegram.Schema.IDocument> Documents { get; set; }
    int? Views { get; set; }

}
