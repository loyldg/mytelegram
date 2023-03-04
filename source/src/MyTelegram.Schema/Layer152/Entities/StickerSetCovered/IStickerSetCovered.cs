// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStickerSetCovered : IObject
{
    MyTelegram.Schema.IStickerSet Set { get; set; }
}
