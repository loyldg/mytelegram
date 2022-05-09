// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IStickerSet : IObject
{
    MyTelegram.Schema.IStickerSet Set { get; set; }
    TVector<MyTelegram.Schema.IStickerPack> Packs { get; set; }
    TVector<MyTelegram.Schema.IDocument> Documents { get; set; }

}
