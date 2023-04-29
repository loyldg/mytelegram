// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStickerSetCovered : IObject
{
    Schema.IStickerSet Set { get; set; }
}
